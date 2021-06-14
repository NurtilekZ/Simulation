using UnityEngine;

namespace _src.Scripts.Controller.Interactable
{
    public class InteractableMovementComponent : MovementComponent
    {
        [SerializeField] private InteractableObject _interactableObject;
        [Header("Fields")]
        [SerializeField] private bool useFriction = false;
        [Header("Direction Arrow")]
        [SerializeField] private Transform _directionArrow;
        [SerializeField] private float _arrowDistance;

        private const float _DefaultFriction = 1f;

        public override void Move()
        {
            if (ReferenceEquals(_interactableObject.dropArea.variableView, null)) return;
            EnableArrow(false);
            velocity = _interactableObject.dropArea.variableView.Value + (!useFriction ? 0 : _DefaultFriction);
            rb.isKinematic = false;
            rb.Sleep();
            rb.velocity = direction * velocity;
        }
        
        private void OnValidate()
        {
            if (ReferenceEquals(_directionArrow, null)) return;
            _directionArrow.position = transform.position + direction * _arrowDistance;
            _directionArrow.rotation = Quaternion.LookRotation(direction, Vector3.up);
            angle = Vector3.Angle(transform.localPosition, direction);
        }

        public void EnableArrow(bool value)
        {
            _directionArrow.gameObject.SetActive(value);
        }

        public void SetAngle(float angle)
        {
            direction = Quaternion.Euler(angle * Vector3.up) * direction;
        }
    }
}