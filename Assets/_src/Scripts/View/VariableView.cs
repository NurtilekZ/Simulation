using UnityEngine;

namespace _src.Scripts.View
{
    public class VariableView : UnitView
    {
        [SerializeField] private float value;
        public float Value
        {
            get => value;
            set => this.value = value;
        }

        protected override void OnValidate()
        {
            SetColor();
        }

        protected override void SetColor()
        {
            textView.text = $"{Value:F1} {physicsProperties.Units[Property]}";
            textView.color = physicsProperties.Colors[Property];
        }
    }
}