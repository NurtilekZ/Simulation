using System;
using _src.Scripts.View;
using _src.Scripts.View.DropAndDrag;
using UnityEngine;

namespace _src.Scripts.Controller.Interactable
{
    public class RotateObject : MonoBehaviour
    {
        [Header("Fields")] 
        [SerializeField] private Vector3 axis;
        [SerializeField] private Vector3 fieldAlignment;
        [Header("References")]
        [SerializeField] private GameObject _rotationArrow;
        [SerializeField] private DropArea _dropArea;
        [SerializeField] private Camera _camera;
        [SerializeField] private InteractableMovementComponent _interactableMovementComponent;
        [SerializeField] private InteractableObject _interactableObject;
        [SerializeField] private GameObject parentObject;

        private void Awake()
        {
            _dropArea.OnDropEvent += Rotate;
            PositionFieldView();
        }
        
        private void PositionFieldView()
        {
            if (_dropArea == null || _camera == null) return;
            Vector3 fieldWorldPosition = transform.position + _camera.transform.up + fieldAlignment;
            _dropArea.transform.position = _camera.WorldToScreenPoint(fieldWorldPosition);
        }

        private void Rotate(VariableView variableView)
        {
            if (ReferenceEquals(parentObject, null))
            {
                transform.Rotate(axis, variableView.Value);
            }
            else
            {
                parentObject.transform.Rotate(axis, variableView.Value);
            }
            _interactableMovementComponent.SetAngle(variableView.Value);
            _interactableObject.SetInitialValues(transform);
        }

        private void OnValidate()
        {
            PositionFieldView();
        }
    }
}