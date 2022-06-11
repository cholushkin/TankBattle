using UnityEngine;
using Resources;

namespace Battle
{
    public class Bullet : MonoBehaviour
    {
        public float Damage;

        private void OnCollisionEnter(Collision collision)
        {
            PrefabHolder.Instance.Instantiate("Explosion", transform.position);
            Destroy(gameObject);
        }
    }
}