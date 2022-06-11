using UnityEngine;
using UnityEngine.Assertions;

namespace Battle
{
    public class WeaponTesla : WeaponBase
    {
        public override void Shoot()
        {
            Assert.IsTrue(BulletSpawners.Length == 1);
            var teslaBullet =
                Instantiate(BulletPrefab, BulletSpawners[0].position, BulletSpawners[0].rotation) as GameObject;
            teslaBullet.transform.parent = BulletSpawners[0];
            teslaBullet.GetComponent<BulletTesla>().Damage = Settings.Damage;
        }
    }
}