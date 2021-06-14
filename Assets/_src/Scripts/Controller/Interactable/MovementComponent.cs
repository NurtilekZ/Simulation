using UnityEngine;

namespace _src.Scripts.Controller.Interactable
{
    public class MovementComponent : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] protected float velocity;
        [SerializeField] protected float trueVelocity;
        [SerializeField] protected Vector3 direction;
        [SerializeField] protected float angle;
        [Header("References")]
        [SerializeField] protected Rigidbody rb;

        public void Setup(MovementHandler movementComponent)
        {
            velocity = movementComponent.velocity;
            direction = movementComponent.direction;
        }

        public virtual void Move()
        {
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
            Gizmos.DrawRay(transform.position, direction * 2);
            
            if (!ReferenceEquals(rb, null)) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}