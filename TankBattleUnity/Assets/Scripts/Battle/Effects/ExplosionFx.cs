using UnityEngine;


namespace Battle
{
    public class ExplosionFx : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 2f);
        }
    }
}
