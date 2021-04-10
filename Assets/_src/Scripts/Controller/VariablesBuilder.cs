using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Controller
{
    public class VariablesBuilder : MonoBehaviour
    {
        public static VariablesBuilder instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void Build(VariableView variable)
        {
            VariableView newVariableView = Instantiate(variable, transform);
            newVariableView.transform.SetAsLastSibling();
        } 
    }
}