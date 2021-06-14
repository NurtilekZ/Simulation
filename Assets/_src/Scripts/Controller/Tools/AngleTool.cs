using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.Controller.Tools
{
    [RequireComponent(typeof(LineRenderer))]
    public class AngleTool : Tool
    {
        [Header("Fields")]
        [SerializeField] private bool isAnglePointActivated;
        [Header("References")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private AnglePoint anglePoint;

        private void OnValidate()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _mainCamera = Camera.main;
        }

        private void Awake()
        {
            anglePoint.OnDragEvent += CalculateAngle;
            anglePoint.OnMouseUpEvent += EnableTool;
        }

        private void OnDestroy()
        {
            anglePoint.OnDragEvent -= CalculateAngle;
            anglePoint.OnMouseUpEvent -= EnableTool;
        }

        private void CalculateAngle(Vector3 pointPosition)
        {
            _lineRenderer.SetPosition(2, pointPosition);
            Vector3 startPosition = _lineRenderer.GetPosition(1); 

            _variable.transform.position = _mainCamera.WorldToScreenPoint(pointPosition + Vector3.up);
            float sign = pointPosition.x < startPosition.x? -1 : 1;
            _variable.Value = Vector3.Angle(pointPosition, startPosition) * sign;
        }

        private void EnableTool()
        {
            isAnglePointActivated = false;
        }

        protected override void ActivateUI(bool value)
        {
            _variable.SetRaycastTarget(value);
        }

        private void Update()
        {
            if (isAnglePointActivated) return;
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
                ActivateUI(true);
                isAnglePointActivated = true;
            }
        }

        private void OnBeginDrag(Vector3 startPosition)
        {
            _isActive = true;
            transform.position = startPosition;
            _lineRenderer.SetPosition(0, startPosition);
            _lineRenderer.SetPosition(1, startPosition);
            _lineRenderer.SetPosition(2, startPosition);
            anglePoint.transform.localPosition = startPosition;
            ActivateUI(false);
        }
        private void OnDrag(Vector3 mousePosition)
        {
            Vector3 middle = Vector3.Lerp(_lineRenderer.GetPosition(1),_lineRenderer.GetPosition(0), 0.5f);
            transform.forward = mousePosition;
            _lineRenderer.SetPosition(1, mousePosition);
            _lineRenderer.SetPosition(2, middle);
            anglePoint.transform.position = _lineRenderer.GetPosition(2);
        }
    }
}