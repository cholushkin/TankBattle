using UnityEngine;

namespace Battle
{
    public class WeaponBase : MonoBehaviour, IWeapon
    {
        public WeaponSettings Settings;
        public Transform[] BulletSpawners;
        public GameObject BulletPrefab;

        public virtual void Shoot()
        {
            foreach (var bulletSpawner in BulletSpawners)
            {
                Rigidbody bullet =
                    (Instantiate(BulletPrefab, bulletSpawner.position, bulletSpawner.rotation) as GameObject)
                        .GetComponent<Rigidbody>();
                bullet.velocity = Settings.Speed*bulletSpawner.forward;
                bullet.gameObject.GetComponent<Bullet>().Damage = Settings.Damage;
            }
        }
    }
}
