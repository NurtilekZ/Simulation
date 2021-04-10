using _src.Scripts.Controller;
using _src.Scripts.Model;
using _src.Scripts.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.Tools
{
    [RequireComponent(typeof(LineRenderer))]
    public class DistanceTool : Tool
    {
        private LineRenderer _lineRenderer;
        private Camera _mainCamera;

        protected override void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _mainCamera = Camera.main;
        }
 
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    OnBeginDrag(hit.point);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    OnDrag(hit.point);
                }
            }
        }

        private void OnBeginDrag(Vector3 startPosition)
        {
            _lineRenderer.SetPosition(0, startPosition);
        }
        private void OnDrag(Vector3 mousePosition)
        {
            _lineRenderer.SetPosition(1, mousePosition);

            _variable.transform.position = _mainCamera.WorldToScreenPoint(mousePosition + Vector3.up);
            _variable.Value = 
                Vector3.Distance(
                    _lineRenderer.GetPosition(0),
                    _lineRenderer.GetPosition(1));
        }

        private void OnEnable()
        {
            _variable.Property = _physicsProperty;
            _variable.gameObject.SetActive(true);
            _variable.Property = PhysicsProperty.Distance;
        }
        private void OnDisable()
        {
            _variable.gameObject.SetActive(false);
        }
    }
}