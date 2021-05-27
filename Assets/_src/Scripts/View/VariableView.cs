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
            set
            {
                this._value = (float)Math.Round(value * 100f) / 100f;
                SetColorAndUnit(_physicalQuantitiesData.PhysicalQuantityModels[Property]);
            }
        }

        public void SetupVariable(VariableView variableView)
        {
            _value = variableView.Value;
            Property = variableView.Property;
        }

        protected override void SetColorAndUnit(PhysicalQuantityModel physicsPropertyModel)
        {
            _textView.color = physicsPropertyModel.color;
            _textView.text = $"{Value} {physicsPropertyModel.unit}";
        }

        public void SetRaycastTarget(bool value)
        {
            _imageView.raycastTarget = value;
        }
    }
}