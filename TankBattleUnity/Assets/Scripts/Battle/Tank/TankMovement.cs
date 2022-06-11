using UnityEngine;

namespace Battle
{
    public class TankMovement : MonoBehaviour
    {
        public float Speed = 6.5f;
        public float RotationSpeed = 180f;

        private Rigidbody RigidBody;
        private float InputMov;
        private float InputTurn;


        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            InputMov = 0f;
            InputTurn = 0f;
        }

        private void Update()
        {
            InputMov = Input.GetAxis("Vertical2");
            InputTurn = Input.GetAxis("Horizontal2");
        }

        private void FixedUpdate()
        {
            Move();
            Turn();
        }

        private void Move()
        {
            Vector3 movement = transform.forward * InputMov * Speed * Time.deltaTime;
            RigidBody.MovePosition(RigidBody.position + movement);
        }

        private void Turn()
        {
            float turn = InputTurn * RotationSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            RigidBody.MoveRotation(RigidBody.rotation * turnRotation);
        }
    }
}