using UnityEngine;

namespace Battle
{
    public class UnitIntruder : UnitBase
    {
        private UnitTank Victim;
        private ObjectFollower FollowerLogic;
        public GameObject Smoke;

        private void Start()
        {
            Victim = GameProcessor.Instance.Hero.GetComponent<UnitTank>();
            FollowerLogic = GetComponent<ObjectFollower>();
        }

        private void Update()
        {
            if (Victim.GetUnitState() == UnitState.Active && FollowerLogic.Target == null)
                FollowerLogic.Target = Victim.gameObject;
            if (Victim.GetUnitState() != UnitState.Active)
                FollowerLogic.Target = null;
            Smoke.SetActive(CurrentHealth < Settings.Health*0.5f);
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            if (collisionInfo.gameObject.CompareTag("Player"))
            {
                Victim.ApplyDamage(Settings.Dangerous*Time.deltaTime);
            }
        }

        protected override void OnEnterState(UnitState prevState, UnitState newState)
        {
            base.OnEnterState(prevState, newState);
        }

        protected override void OnExitState(UnitState getUnitState)
        {
            base.OnExitState(getUnitState);
        }
    }
}