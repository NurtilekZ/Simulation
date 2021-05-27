using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    public class VariablesBuilder : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour variableViewPrefab;
        
        public delegate void NewVariable(VariablesBuilder sender, VariableView newVariable);
        public event NewVariable OnAddNewVariable;

        public void AddNewVariable(VariableView variable)
        {
            VariableView newVariableView = Instantiate(variableViewPrefab.gameObject, transform).GetComponent<VariableView>();
            newVariableView.SetupVariable(variable);
            newVariableView.transform.SetAsLastSibling();
            OnAddNewVariable?.Invoke(this, variable);
        }
    }
}