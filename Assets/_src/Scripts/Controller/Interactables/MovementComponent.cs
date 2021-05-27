using UnityEngine;

namespace _src.Scripts.Controller.Interactables
{
    public class MovementComponent : MonoBehaviour
    {
        public float velocity;
        public float trueVelocity;
        public Vector3 direction;
        public Rigidbody rb;

        public void Move()
        {
            if (TryGetComponent<Interactable>(out var interactable))
            {
                if (interactable.dropArea.variableView == null) return;
                velocity = interactable.dropArea.variableView.Value;
            }
            rb.Sleep();
            rb.velocity = direction * velocity;
        }

        private void Update()
        {
            trueVelocity = rb.velocity.x;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, direction * velocity);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}