using System;
using _src.Scripts.Controller.Formulas;
using _src.Scripts.Controller.Services;
using _src.Scripts.Controller.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.View.DropAndDrag
{
    public class DropArea : MonoBehaviour, IDropHandler, ISubscriber
    {
        [SerializeField] private bool _propertySensitive = true;
        [SerializeField] private FieldView _fieldView;
        
        public VariableView variableView;
        public event Action<VariableView> OnDropEvent;

        public void OnDrop(PointerEventData eventData)
        {
            if (!_propertySensitive) return;
            if (!eventData.pointerDrag.TryGetComponent<VariableView>(out var droppedVariable)) return;
            if (_fieldView.Property != droppedVariable.Property) return;
            if (variableView != null)
            {
                ServiceLocator.Current
                    .GetService<EventManager>()
                    .Publish(PublisherType.RETURN_TO_VARIABLES, this, variableView);
                variableView = null;
            }
            AttachVariable(droppedVariable);
            OnDropEvent?.Invoke(droppedVariable);
        }

        private void AttachVariable(VariableView droppedVariable)
        {
            variableView = droppedVariable;
            variableView.transform.SetParent(transform);
            variableView.transform.position = transform.position;
            if (transform.parent.TryGetComponent<Formula>(out var formula))
            {
                formula.Calculate();
            }
        }

        public void OnPublish<T>(PublisherType publisherType, Component sender, T param = default)
        {
            VariableView newVariableView = param as VariableView;
            AttachVariable(newVariableView);
        }

        public void OnEnable()
        {
            if (!_propertySensitive)
                ServiceLocator.Current
                    .GetService<EventManager>().
                    RegisterListener(PublisherType.RETURN_TO_VARIABLES,this);
        }

        public void OnDisable()
        {
            if (!_propertySensitive)
                ServiceLocator.Current
                    .GetService<EventManager>()
                    .UnregisterListener(PublisherType.RETURN_TO_VARIABLES,this);
        }
    }
}
