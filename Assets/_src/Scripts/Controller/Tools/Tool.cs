using _src.Scripts.Model;
using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Controller.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [Header("Common Tool Properties")]
        [SerializeField] protected bool _isActive;
        [Header("Individual Tool Properties")]
        [SerializeField] protected PhysicalQuantity _physicalQuantity;
        [SerializeField] protected VariableView _variable;

        protected abstract void Awake();

        public void SelectTool(bool triggerValue)
        {
            gameObject.SetActive(triggerValue);
        }
        
        public virtual void OnEnable()
        {
            _variable.Property = _physicalQuantity;
            _variable.gameObject.SetActive(true);
        }

        public virtual void OnDisable()
        {
            _variable?.gameObject.SetActive(false);
        }

        protected abstract void ActivateTool(bool value);
    }
}