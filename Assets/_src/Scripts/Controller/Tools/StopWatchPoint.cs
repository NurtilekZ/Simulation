using _src.Scripts.Controller.Interactables;
using UnityEngine;

namespace _src.Scripts.Controller.Tools
{
    public class StopWatchPoint : MonoBehaviour
    {
        private Collider _collider;
        private Renderer _renderer;

        public delegate void Stopwatch(StopWatchPoint stopWatchPoint, Renderer newTargetRenderer);
        public event Stopwatch StopwatchEvent;

        private void InvokeStopwatchEvent(Renderer newTargetRenderer)
        {
            StopwatchEvent?.Invoke(this, newTargetRenderer);
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _renderer = GetComponent<Renderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Obstacle>() == null) return;
            InvokeStopwatchEvent(other.GetComponent<Renderer>());
        }

        public void ActivatePoint(bool value)
        {
            if (ReferenceEquals(_collider, null) ||
                ReferenceEquals(_renderer, null))
                Awake();

            _collider.enabled = value;
            _renderer.material.color = value ? Color.magenta : Color.white;
        }
    }
}