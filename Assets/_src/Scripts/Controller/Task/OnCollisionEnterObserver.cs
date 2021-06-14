using _src.Scripts.Controller.Interactable;
using UnityEngine;
using UnityEngine.Events;

namespace _src.Scripts.Controller.Task
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Collider))]
    public class OnCollisionEnterObserver : Observer
    {
        [SerializeField] private bool disableObjectOnCollision;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TriggerObject triggerObject;
        
        [SerializeField] private UnityEvent OnNotify; 

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<InteractableObject>() &&
                triggerObject == TriggerObject.INTERACTABLE)
            {
                NotifyObserver(this);
            }
            else if (other.gameObject.GetComponent<Obstacle>() &&
                     triggerObject == TriggerObject.OBSTACLE)
            {
                NotifyObserver(this);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<InteractableObject>() &&
                triggerObject == TriggerObject.INTERACTABLE)
            {
                NotifyObserver(this);
            }
            else if (other.GetComponent<Obstacle>() &&
                     triggerObject == TriggerObject.OBSTACLE)
            {
                NotifyObserver(this);
            }

        }

        public override void NotifyObserver(object param = default)
        {
            base.NotifyObserver(param);
            OnNotify?.Invoke();
            if (disableObjectOnCollision)
            {
                gameObject.SetActive(false);
            }
        }

        private void ChangeMaterial()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            _meshRenderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor("_TintColor",Color.green);
            _meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }

        protected override void OnEnable() { }
        protected override void OnDisable() { }
    }

    internal enum TriggerObject
    {
        OBSTACLE,
        INTERACTABLE
    }
}
