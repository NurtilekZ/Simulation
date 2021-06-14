using _src.Scripts.View.DropAndDrag;
using UnityEngine;

namespace _src.Scripts.Controller.Interactable
{
    public class InteractableObject : MonoBehaviour
    {
        [Header("References")]
        public DropArea dropArea;
        [SerializeField] private Camera cameraCache;
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Start()
        {
            SetInitialValues(transform);
            PositionFieldView();
        }

        private void PositionFieldView()
        {
            if (dropArea == null || cameraCache == null) return;
            Vector3 fieldWorldPosition = transform.position + cameraCache.transform.up;
            dropArea.transform.position = cameraCache.WorldToScreenPoint(fieldWorldPosition);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out Obstacle obstacle)) return;
            ResetObject();
        }

        public void ResetObject()
        {
            GetComponent<Rigidbody>().Sleep();
            transform.SetPositionAndRotation(_initialPosition, _initialRotation);
            if (!TryGetComponent(out InteractableMovementComponent interactable)) return;
            interactable.EnableArrow(true);
            gameObject.SetActive(true);
        }

        private void OnValidate()
        {
            PositionFieldView();
        }

        public void SetInitialValues(Transform newTransform)
        {
            _initialPosition = newTransform.position;
            _initialRotation = newTransform.rotation;
        }
    }
}