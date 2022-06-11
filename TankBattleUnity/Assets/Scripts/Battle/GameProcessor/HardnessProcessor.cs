using UnityEngine;

namespace Battle
{
    public class HardnessProcessor : MonoBehaviour
    {
        public Hardness HarndessPoint0;
        public Hardness HarndessPoint1;
        public float GrowTime;

        private float CurrentTime;
        private float Factor;

        public Hardness GetCurrentHardness()
        {
            return Hardness.Lerp(HarndessPoint0, HarndessPoint1, Factor);
        }

        public void Update()
        {
            CurrentTime += Time.deltaTime;
            Factor = CurrentTime/GrowTime;
            Factor = Mathf.Clamp(Factor, 0f, 1f);
        }
    }
}
