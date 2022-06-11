using UnityEngine;

namespace Battle
{
    public class WeaponSwitcher : MonoBehaviour
    {
        public int CurrentWeapon;
        public GameObject[] Weapons;

        private void Update()
        {
            if (Input.GetButtonUp("PrevWeapon"))
            {
                CurrentWeapon = CurrentWeapon == 0 ? Weapons.Length - 1 : CurrentWeapon - 1;
                ChangeWeapon();
            }
            if (Input.GetButtonUp("NextWeapon"))
            {
                CurrentWeapon = (CurrentWeapon + 1) % Weapons.Length;
                ChangeWeapon();
            }
        }

        private void ChangeWeapon()
        {
            foreach (var weapon in Weapons)
                weapon.SetActive(false);
            Weapons[CurrentWeapon].SetActive(true);
            GameProcessor.Instance.OnWeaponChanged(CurrentWeapon);
        }
    }
}