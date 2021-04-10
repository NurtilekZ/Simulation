using _src.Scripts.Controller;
using _src.Scripts.Model;
using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] protected VariableView _variable;
        [SerializeField] protected PhysicsProperty _physicsProperty;

        protected abstract void Awake();
        
        public void Select(bool toggleValue)
        {
            gameObject.SetActive(toggleValue);
        }

        public virtual void AddNewVariable()
        {
            VariablesBuilder.instance.Build(_variable);
        }
    }
}