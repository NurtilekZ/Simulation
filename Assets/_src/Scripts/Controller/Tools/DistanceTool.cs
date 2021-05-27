using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.Controller.Tools
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

        protected override void ActivateTool(bool value)
        {
            _variable.SetRaycastTarget(value);
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _isActive = false;
                return;
            }
            
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    OnBeginDrag(hit.point);
                }
            }

            else if (Input.GetMouseButton(0) && _isActive)
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    OnDrag(hit.point);
                }
            }
            
            else if(Input.GetMouseButtonUp(0) && _isActive)
            {
                ActivateTool(true);
            }
        }

        private void OnBeginDrag(Vector3 startPosition)
        {
            _isActive = true;
            _lineRenderer.SetPosition(0, startPosition);
            _lineRenderer.SetPosition(1, startPosition);
            ActivateTool(false);
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
    }
}