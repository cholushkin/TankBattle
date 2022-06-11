using System;

namespace Battle
{
    public interface IWeapon
    {
        void Shoot();
    }

    [Serializable]
    public class WeaponSettings
    {
        public float Damage;
        public float Speed;
        public float Delay;
    }
}