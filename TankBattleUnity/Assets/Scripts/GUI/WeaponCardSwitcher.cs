using UnityEngine;
using UnityEngine.UI;

namespace Battle.GUI
{
    public class WeaponCardSwitcher : MonoBehaviour
    {
        public Image[] Cards;

        public void Start()
        {
            SelectCard(0);
        }

        public void SelectCard(int index)
        {
            for (int i = 0; i < 3; i++)
            {
                Cards[i].color = Color.gray;
                Cards[i].rectTransform.localScale = i == index
                    ? Vector3.one*1.5f
                    : Vector3.one*0.85f;
            }
        }

        public void Semaphore(bool isReloading)
        {
            for (int i = 0; i < 3; i++)
                if (Cards[i].rectTransform.localScale.x > 1f) // active?
                {
                    Cards[i].color = isReloading ? Color.gray : Color.white;
                    return;
                }
        }
    }
}
