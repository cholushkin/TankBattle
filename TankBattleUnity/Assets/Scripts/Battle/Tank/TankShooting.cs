using UnityEngine;
using System.Collections;


namespace Battle
{
    public class TankShooting : MonoBehaviour
    {
        public WeaponBase Weapon;
        private bool IsReloading;


        private void OnEnable()
        {
            // on select weapon first is to do is reloading
            StartCoroutine(Reloading());
        }

        private void Update()
        {
            if (IsReloading)
                return;
            if (Input.GetButtonUp("Fire1"))
            {
                Weapon.Shoot();
                StartCoroutine(Reloading());
            }
        }

        private IEnumerator Reloading()
        {
            IsReloading = true;
            GameProcessor.Instance.OnWeaponReloading(IsReloading);
            yield return new WaitForSeconds(Weapon.Settings.Delay);
            IsReloading = false;
            GameProcessor.Instance.OnWeaponReloading(IsReloading);
        }
    }
}