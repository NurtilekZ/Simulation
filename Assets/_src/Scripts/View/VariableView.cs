using System;
using _src.Scripts.Model;
using UnityEngine;

namespace _src.Scripts.View
{
    public class VariableView : FieldView
    {
        [SerializeField] private float _value;
        public float Value
        {
            get => _value;
            set => OnValueChange(value);
        }

        private void OnValueChange(float value)
        {
            _value = (float)Math.Round(value * 100f) / 100f;
            SetColor(_physicalQuantitiesData.physicalQuantityModels[Property]);
        }

        public void SetupVariable(VariableView variableView)
        {
            _value = variableView.Value;
            Property = variableView.Property;
        }

        protected override void SetColor(PhysicalQuantityModel physicalQuantityModel)
        {
            _textView.color = physicalQuantityModel.color;
            SetValueAndUnit(physicalQuantityModel);
        }

        private void SetValueAndUnit(PhysicalQuantityModel physicalQuantityModel)
        {
            _textView.text = $"{Value} {physicalQuantityModel.unit}";
        }

        public void SetRaycastTarget(bool value)
        {
            _imageView.raycastTarget = value;
        }
    }
}