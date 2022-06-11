using UnityEngine;
using Resources;

namespace Battle
{
    public class UnitTank : UnitBase
    {
        public int Lifes;

        protected override void Die()
        {
            PrefabHolder.Instance.Instantiate("Explosion", transform.position);
            GotoState(UnitState.Dead);
        }

        protected override void OnEnterState(UnitState prevState, UnitState newState)
        {
            base.OnEnterState(prevState, newState);
            if (prevState == UnitState.Respawning && newState == UnitState.Active)
                gameObject.transform.LookAt(new Vector3(0, 0, GameObject.Find("Arena/ArenaCenter").transform.position.z));
            if (newState == UnitState.Dead)
            {
                --Lifes;
                gameObject.SetActive(false);
            }
        }

        protected override void OnExitState(UnitState getUnitState)
        {
        }
    }
}
