using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.Controller.Tools
{
    public class StopWatchTool : Tool
    {
        [Header("Fields")]
        [SerializeField] private float timePeriod;
        [Header("References")]
        [SerializeField] private Renderer _targetRenderer;
        [SerializeField] private Material _measuredObjectDefaultMaterial;
        [SerializeField] private Material _highlightMaterial;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private StopWatchPoint _startPoint;
        [SerializeField] private StopWatchPoint _endPoint;

        private IEnumerator _timerCoroutine;

        private void OnValidate()
        {
            _mainCamera = Camera.main;
            _timerCoroutine = StartTimer();
        }
        
        private IEnumerator StartTimer()
        {
            while (true)
            {
                _variable.Value += timePeriod; 
                yield return new WaitForSeconds(timePeriod);
            }
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
                ActivateUI(true);
            }
        }

        protected override void ActivateUI(bool value)
        {
            _startPoint.ActivatePoint(value);
            _endPoint.ActivatePoint(value);
            _variable.SetRaycastTarget(value);
        }
        private void OnBeginDrag(Vector3 mousePosition)
        {
            _isActive = true;
            _variable.Value = 0;
            if (!_startPoint.gameObject.activeSelf)
            {
                _startPoint.gameObject.SetActive(true);
            }
            _startPoint.transform.position = mousePosition;
            _endPoint.transform.position = mousePosition;
            ActivateUI(false);
        }
        private void OnDrag(Vector3 mousePosition)
        {
            if (!_endPoint.gameObject.activeSelf)
            {
                _endPoint.gameObject.SetActive(true);
            }

            var endPointTransform = _endPoint.transform;
            endPointTransform.position = mousePosition;
            _variable.transform.position = _mainCamera.WorldToScreenPoint(endPointTransform.position + Vector3.up);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            _startPoint.StopwatchEvent += Check;
            _endPoint.StopwatchEvent += Check;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _startPoint.StopwatchEvent -= Check;
            _endPoint.StopwatchEvent -= Check;
        }

        private void Check(StopWatchPoint stopWatchPoint, Renderer newTargetRenderer)
        {
            if (_targetRenderer == null && stopWatchPoint == _startPoint)
            {
                _targetRenderer = newTargetRenderer;
                _measuredObjectDefaultMaterial = newTargetRenderer.material;
                newTargetRenderer.material = _highlightMaterial;
                _variable.Value = 0;
                StartCoroutine(_timerCoroutine);
                _startPoint.ActivatePoint(false);
            }
            else if (_targetRenderer == newTargetRenderer && stopWatchPoint == _endPoint)
            {
                _targetRenderer.GetComponent<Renderer>().material = _measuredObjectDefaultMaterial;
                _targetRenderer = null;
                StopCoroutine(_timerCoroutine);
                _endPoint.ActivatePoint(false);
            }
        }
    }
}