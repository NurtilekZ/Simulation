using System.Collections.Generic;
using _src.Scripts.View;
using B83;
using TMPro;
using UnityEngine;

namespace _src.Scripts.Formulas
{
    [ExecuteAlways]
    public class Formula : MonoBehaviour
    {
        [SerializeField] private List<Transform> formulaList;
        [SerializeField] private UnitView _fieldPrefab;
        [SerializeField] private TextMeshProUGUI _symbolText;
        
        public VariableView resultVariableView;
        
        public string formula;
        
        public void RemoveItem()
        {
            Transform item = transform.GetChild(transform.childCount - 1);
            if (formulaList.Contains(item))
            {
                formulaList.Remove(item);
            }
            DestroyImmediate(item.gameObject);
        }

        public void AddField()
        {
            Instantiate(_fieldPrefab, transform);
        }
        
        public void AddSign()
        {
            Instantiate(_symbolText, transform);
        }

        private void OnTransformChildrenChanged()
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                Transform item = transform.GetChild(i);
                if (!formulaList.Contains(item))
                {
                    formulaList.Add(item);
                }
            }
            
            UpdateFormula();
        }

        public void UpdateFormula()
        {
            formula = "";
            for (int i = 0; i < formulaList.Count; i++)
            {
                GameObject item = formulaList[i].gameObject;
                if (!Application.isPlaying && item == null)
                {
                    formulaList.RemoveAt(i);
                }

                if (item.TryGetComponent<DropArea>(out var field))
                {
                    formula += field.variableView?.Value;
                }
                else if (item.TryGetComponent<TextMeshProUGUI>(out var text))
                {
                    formula += text.text;
                }
            }

            Calculate();
        }

        private void Calculate()
        {
            ExpressionParser expressionParser = new ExpressionParser();
            resultVariableView.Value = (float)expressionParser.Evaluate(formula);
        }
    }
}
