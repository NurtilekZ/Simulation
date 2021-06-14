using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.Controller.Tools
{
    public class AnglePoint : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private bool _isSelected;
        [Header("References")]
        [SerializeField] private Camera _camera;
        
        public event Action<Vector3> OnDragEvent;
        public event Action OnMouseUpEvent;
        
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
                _isSelected = ReferenceEquals(hit.collider.gameObject, gameObject);
            }
            else if (Input.GetMouseButton(0) && _isSelected)
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
                    OnDrag(hit.point);
            }
            else if (Input.GetMouseButtonUp(0) && _isSelected)
            {
                _isSelected = false;
                OnMouseUpEvent.Invoke();
            }

        }
        
        private void OnDrag(Vector3 hitPoint)
        {
            transform.position = new Vector3(hitPoint.x,transform.position.y,hitPoint.z);
            OnDragEvent?.Invoke(transform.position);
        }
    }
}