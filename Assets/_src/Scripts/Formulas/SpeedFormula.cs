using System.Collections.Generic;
using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Formulas
{
    public class Formula : MonoBehaviour
    {
        [SerializeField] private List<VariableView> variablesList = new List<VariableView>();
        [SerializeField] private VariableView variablePrefab;
        
        public string formula;
        
        [ContextMenu("Remove Variable")]
        public void RemoveValue()
        {
            VariableView variableView =  transform.GetChild(transform.childCount).GetComponent<VariableView>();
            if (variablesList.Contains(variableView))
            {
                variablesList.Remove(variableView);
            }
            Destroy(variableView);
        }

        [ContextMenu("Add Variable")]
        public void AssignValue()
        {
            VariableView variableView = (VariableView) Instantiate(variablePrefab, transform);
            variablesList.Add(variableView);
        }

        private void Calculate()
        {
            
        }
    }
}
