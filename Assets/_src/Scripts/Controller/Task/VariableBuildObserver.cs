using System;
using _src.Scripts.Controller.Systems;
using _src.Scripts.Model;
using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Controller.Task
{
    public class VariableBuildObserver : Observer
    {
        [SerializeField] private float _targetValue;
        [SerializeField] private PhysicalQuantity _targetProperty;
        [SerializeField] private VariablesBuilder _variablesBuilder;
        
        protected override void OnEnable()
        {
            _variablesBuilder.OnAddNewVariable += CheckNewVariable;
        }
        
        protected override void OnDisable()
        {
            _variablesBuilder.OnAddNewVariable -= CheckNewVariable;
        }

        private void CheckNewVariable(VariablesBuilder sender, VariableView newVariable)
        {
            if (newVariable.Property != _targetProperty) return;
            if (!(Math.Abs(newVariable.Value - _targetValue) < 0.5f)) return;
            NotifyObserver();
            OnDisable();
        }
    }
}