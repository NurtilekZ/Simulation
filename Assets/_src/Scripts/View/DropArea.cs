using _src.Scripts.Controller;
using _src.Scripts.Controller.Formulas;
using _src.Scripts.Controller.Services;
using _src.Scripts.Controller.Systems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _src.Scripts.View
{
    public class DropArea : MonoBehaviour, IDropHandler, IListener
    {
        [SerializeField] private bool _propertySensitive = true;
        [SerializeField] private FieldView _fieldView;
        
        public VariableView variableView;

        public void OnDrop(PointerEventData eventData)
        {
            if (!_propertySensitive) return;
            if (!eventData.pointerDrag.TryGetComponent<VariableView>(out var droppedVariable)) return;
            if (_fieldView.Property != droppedVariable.Property) return;
            if (variableView != null)
            {
                ServiceLocator.Current
                    .GetService<EventManager>()
                    .Raise(Event_Type.RETURN_TO_VARIABLES, this, variableView);
                variableView = null;
            }
            AttachVariable(droppedVariable);
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

        public void OnEvent<T>(Event_Type eventType, Component sender, T param = default)
        {
            VariableView newVariableView = param as VariableView;
            AttachVariable(newVariableView);
        }

        public void OnEnable()
        {
            if (!_propertySensitive)
                ServiceLocator.Current
                    .GetService<EventManager>().
                    RegisterListener(Event_Type.RETURN_TO_VARIABLES,this);
        }

        public void OnDisable()
        {
            if (!_propertySensitive)
                ServiceLocator.Current
                    .GetService<EventManager>()
                    .UnregisterListener(Event_Type.RETURN_TO_VARIABLES,this);
        }
    }
}
