using UnityEngine;
using UnityEngine.UI;

namespace Battle.GUI
{
    public class ProgressBar : MonoBehaviour
    {
        public float Max;
        public float CurVal;
        public Image Image;

        public void Set(float val)
        {
            CurVal = val;
            Image.fillAmount = CurVal / Max;
        }
    }
}