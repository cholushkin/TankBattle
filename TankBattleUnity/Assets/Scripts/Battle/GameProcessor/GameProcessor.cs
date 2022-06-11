using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Alg;
using Complete;
using RandomUtils;
using UnityEngine.Assertions;


namespace Battle
{
    public class GameProcessor : Singleton<GameProcessor>
    {
        public Transform Arena;
        public GameObject Hero;
        public HardnessProcessor HardnessProcessor;
        public CameraControl CameraController;
        public GameObject Enemies;
        public GameStateGameplay GameplayState;

        public int ScoreAwardKillEnemy;
        public bool IsNewTopScore;

        private RandomHelper Rnd = new RandomHelper(System.Environment.TickCount);

        // accessor shortcuts
        private Spawner[] TankSpawners;
        private Spawner[] EnemySpawners;
        private UnitTank Tank;


        private void Start()
        {
            Assert.IsNotNull(CameraController);
            Assert.IsNotNull(Hero);
            Assert.IsNotNull(HardnessProcessor);
            Assert.IsNotNull(Enemies);

            Tank = Hero.GetComponent<UnitTank>();

            InitSpawners();
            InitScores();

            CameraFocus(Hero.GetComponent<Transform>());
            StartCoroutine(GameLoop());
        }

        private void InitScores()
        {
            SimpleUserAccount.Load();
            SimpleUserAccount.UserData.LastScore = 0;
            GameplayState.TopScore.text = "Top Score: " + SimpleUserAccount.UserData.TopScore;
        }

        private void Update()
        {
            GameplayState.EnergyProgressBar.Set(Tank.CurrentHealth);
            GameplayState.LivesProgressBar.Set(Tank.Lifes);
        }

        private void InitSpawners()
        {
            Spawner.DbgSpawnerCounter = 0;
            var allSpawners = Arena.GetComponentsInChildren<Spawner>();
            TankSpawners = allSpawners.Where(x => x.SpawnerSettings[0].Unit == UnitType.Tank).ToArray();
            EnemySpawners = allSpawners.Where(x => x.SpawnerSettings[0].Unit != UnitType.Tank).ToArray();
        }
        
        private void CameraFocus(List<Transform> target) // todo: focus on the group of items
        {
        }

        private void CameraFocus(Transform target)
        {
            CameraController.m_Targets = new[] { target };
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            OnStartSession();
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());
            OnEndSession();
        }

        private void OnEndSession()
        {
            GameStateManager.Instance.Start(typeof(GameStateResult), false);
        }

        private void OnStartSession()
        {
        }

        private bool CheckEndingConditions()
        {
            return Tank.Lifes > 0;
        }


        private IEnumerator RoundStarting()
        {
            yield return new WaitForSeconds(0.1f);
        }

        public bool CanSpawnEnemy()
        {
            var curHardness = HardnessProcessor.GetCurrentHardness();
            var enemyCount = Enemies.transform.childCount;
            return (enemyCount < curHardness.MobCount);
        }

        private static UnitType[] allUnitTypes = { UnitType.IntruderBase, UnitType.IntruderMed, UnitType.IntruderHard };

        private IEnumerator RoundPlaying()
        {
            while (CheckEndingConditions())
            {
                // ----- process tank (respawn)
                if (Tank.GetUnitState() == UnitState.Oblivion || Tank.GetUnitState() == UnitState.Dead)
                {
                    if (Tank.GetUnitState() == UnitState.Dead)
                        GameplayState.Messenger.ShowMessage("Boom!", "Remaining lives: " + Tank.Lifes);
                    // find random tank spawner and spawn tank
                    TankSpawners[Random.Range(0, TankSpawners.Length)].Spawn(UnitType.Tank);
                    // now state of tank is spawning
                }

                // ----- process enemies
                // --- respawn
                if (CanSpawnEnemy())
                {
                    // choose unit type relative current hardness
                    var curHardness = HardnessProcessor.GetCurrentHardness();
                    var index =
                        Rnd.SpawnEvent(new float[]
                        {curHardness.ProbabilityEasyMob, curHardness.ProbabilityMedMob, curHardness.ProbabilityHardMob});
                    var unitTypeToSpawn = allUnitTypes[index];
                    EnemySpawners[Random.Range(0, EnemySpawners.Length)].Spawn(unitTypeToSpawn);
                }

                // return on the next frame.
                yield return null;
            }
        }
        
        private IEnumerator RoundEnding()
        {
            GameplayState.Messenger.ShowMessage("You are dead", "No more lives :(");
            SimpleUserAccount.Save();
            yield return new WaitForSeconds(3f);
        }

        // todo: as events
        public void OnEnemyDie()
        {
            SimpleUserAccount.UserData.LastScore += ScoreAwardKillEnemy;
            if (SimpleUserAccount.UserData.LastScore > SimpleUserAccount.UserData.TopScore)
            {
                SimpleUserAccount.UserData.TopScore = SimpleUserAccount.UserData.LastScore;
                IsNewTopScore = true;
            }
            GameplayState.CurrentScore.text = "Score: " + SimpleUserAccount.UserData.LastScore;
            GameplayState.TopScore.text = "Top Score: " + SimpleUserAccount.UserData.TopScore;
        }

        public void OnWeaponChanged(int index)
        {
            GameplayState.WeaponCardSwitcher.SelectCard(index);
        }

        public void OnWeaponReloading(bool isReloading)
        {
            GameplayState.WeaponCardSwitcher.Semaphore(isReloading);
        }
    }
}