using UnityEngine;

namespace Battle
{
    public class Rotator : MonoBehaviour
    {
        public float RotationSpeed;

        void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed, Space.World);
        }
    }
}
