using Resources;
using UnityEngine;

namespace Battle
{
    public abstract class UnitBase : MonoBehaviour, IUnit
    {
        public UnitSettings Settings;
        public float CurrentHealth;

        private UnitState CurrentState = UnitState.Oblivion;


        public UnitType GetUnitType()
        {
            return UnitType.Unknown;
        }

        public UnitState GetUnitState()
        {
            return CurrentState;
        }

        public void GotoState(UnitState state)
        {
            var prevState = GetUnitState();
            OnExitState(prevState);
            CurrentState = state;
            OnEnterState(prevState, state);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
                ApplyDamage(collision.gameObject.GetComponent<Bullet>().Damage);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet")) // tesla bullet
                ApplyDamage(other.gameObject.GetComponent<Bullet>().Damage);
            //if (other.CompareTag("Player"))
            //    ApplyDamage(other.gameObject.GetComponent<Bullet>().Damage);
        }

        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage * (1f - Settings.Armour);

            if (CurrentHealth <= 0)
                Die();
        }

        protected virtual void Die()
        {
            PrefabHolder.Instance.Instantiate("Explosion", transform.position);
            Destroy(gameObject);
            GameProcessor.Instance.OnEnemyDie();
        }

        protected virtual void OnEnterState(UnitState prevState, UnitState newState)
        {
            if (newState == UnitState.Respawning)
                CurrentHealth = Settings.Health;
        }

        protected virtual void OnExitState(UnitState state)
        {

        }
    }
}