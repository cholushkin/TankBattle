using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RandomUtils;
using Resources;
using UnityEngine.Assertions;

namespace Battle
{
    public class Spawner : MonoBehaviour
    {
        private Queue<SpawnerSetting> Queue = new Queue<SpawnerSetting>();
        public static int DbgSpawnerCounter;
        private bool IsSpawning;

        [Serializable]
        public class SpawnerSetting
        {
            public UnitType Unit;
            public float SpawnDelay;
        }

        public static RandomHelper Rnd = new RandomHelper(System.Environment.TickCount);
        public SpawnerSetting[] SpawnerSettings;



        public void Spawn(UnitType unitType)
        {
            var entry = SpawnerSettings.FirstOrDefault(x => x.Unit == unitType);
            Assert.IsNotNull(entry, "spawner doesn't support such unit type");
            Queue.Enqueue(entry);
            ProcessQueue();
        }

        public void Update()
        {
            ProcessQueue();
        }

        private void ProcessQueue()
        {
            // if spawn the tank then spawn anyway, if spawn enemy then check count of enemy on the arena first
            if (Queue.Count > 0 && !IsSpawning &&
                (GameProcessor.Instance.CanSpawnEnemy() || Queue.Peek().Unit == UnitType.Tank))
            {
                IsSpawning = true;
                StartCoroutine(Spawning(Queue.Dequeue()));
            }
        }

        private IEnumerator Spawning(SpawnerSetting spawnSetting)
        {
            var itemOnShelf = SpawnType(spawnSetting.Unit);
            itemOnShelf.GetComponent<IUnit>().GotoState(UnitState.Respawning);
            yield return new WaitForSeconds(spawnSetting.SpawnDelay);

            // activate unit
            itemOnShelf.SetActive(true);
            itemOnShelf.GetComponent<IUnit>().GotoState(UnitState.Active);

            yield return null; // skip frame
            IsSpawning = false;
        }

        private GameObject SpawnType(UnitType unitType)
        {
            // todo: factory 
            if (unitType == UnitType.Tank)
            {
                const float tankYPosition = -3f;
                var heroObj = GameProcessor.Instance.Hero;
                heroObj.SetActive(false);
                heroObj.transform.localPosition = new Vector3(transform.position.x, tankYPosition,
                    transform.position.z);
                return heroObj;
            }
            if (unitType == UnitType.IntruderBase || unitType == UnitType.IntruderMed ||
                unitType == UnitType.IntruderHard)
            {
                // get prefab name
                var prefabName = "MonsterIntruderBase";
                if (unitType == UnitType.IntruderMed)
                    prefabName = "MonsterIntruderMed";
                if (unitType == UnitType.IntruderHard)
                    prefabName = "MonsterIntruderHard";

                // instantiate prefab
                var obj = InstantiatePrefab(prefabName);
                obj.transform.parent = GameProcessor.Instance.Enemies.transform;
                obj.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f,
                    transform.localPosition.z);

                // activate unit
                obj.SetActive(false);
                return obj;
            }
            return null;
        }

        public GameObject InstantiatePrefab(string prefabName)
        {
            var prefab = PrefabHolder.Instance.GetPrefab(prefabName);
            var obj = Instantiate(prefab);
            obj.name = prefabName + "." + DbgSpawnerCounter++;
            return obj;
        }
    }
}