using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _src.Scripts.Model;
using _src.Scripts.View;
using org.mariuszgromada.math.mxparser;
using TMPro;
using UnityEngine;

namespace _src.Scripts.Controller.Formulas
{
    public class Formula : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private PhysicalQuantity _physicalQuantity;
        [SerializeField] private VariableView resultVariableView;
        [SerializeField] private List<DropArea> fields;
        [SerializeField] private string placeholderKey;
        [TextArea]
        [SerializeField] private string formula;

        private void OnValidate()
        {
            formula = string.Empty;
            titleText.text = _physicalQuantity.ToString();
            resultVariableView.Property = _physicalQuantity;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<DropArea>(out var dropArea))
                {
                    formula += placeholderKey;
                    if (fields.Contains(dropArea)) continue;
                    fields.Add(dropArea);
                }
                else if (transform.GetChild(i).TryGetComponent<TextMeshProUGUI>(out var text))
                {
                    formula += text.text;
                }
            }
        }

        public void Calculate()
        {
            if (fields.Any(variable => variable.variableView == null)) return;
            
            string tempFormula = formula;
            Regex regex = new Regex(placeholderKey);
            for (int i = 0; i < fields.Count;  i++)
            {
                tempFormula = regex.Replace(tempFormula, ""+fields[i].variableView.Value, 1);
            }
            regex = new Regex(",");
            tempFormula = regex.Replace(tempFormula, ".");
            Debug.Log(tempFormula);
            Expression expression = new Expression(tempFormula);
            resultVariableView.Value = (float)expression.calculate();
            Debug.Log((float)expression.calculate());
        }
    }
}