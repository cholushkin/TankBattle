using UnityEngine;

namespace Battle
{
    public class ObjectFollower : MonoBehaviour
    {
        public GameObject Target;
        public float Speed;
        private Rigidbody Body;

        private void Start()
        {
            Body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (null == Target)
            {
                Body.velocity = new Vector3(0, Body.velocity.y, 0); // keep falling but stop moving in horizontal plane
                return;
            }

            Vector3 dir = Target.transform.position - gameObject.transform.position;
            if (dir.sqrMagnitude < 0.01f)
                return;

            Body.velocity = Vector3.down + dir.normalized*Time.fixedDeltaTime*Speed;
        }
    }
}