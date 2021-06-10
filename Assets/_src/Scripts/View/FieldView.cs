using _src.Scripts.Controller.Systems;
using _src.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _src.Scripts.View
{
    [ExecuteInEditMode]
    public class FieldView : MonoBehaviour
    {
        [SerializeField] protected Image _imageView;

        [SerializeField] protected TextMeshProUGUI _textView;
        [SerializeField] protected PhysicalQuantity _physicalQuantity;
        [SerializeField] protected PhysicalQuantitiesData _physicalQuantitiesData;
        public PhysicalQuantity Property
        {
            get => _physicalQuantity;
            set => OnValueChange(value);
        }

        private void OnValueChange(PhysicalQuantity value)
        {
            _physicalQuantity = value;
            if (_physicalQuantitiesData.physicalQuantityModels.ContainsKey(_physicalQuantity))
                SetColor(_physicalQuantitiesData.physicalQuantityModels[_physicalQuantity]);
        }

        protected virtual void SetColor(PhysicalQuantityModel physicalQuantityModel)
        {
            _imageView.color = physicalQuantityModel.color;
            SetUnit(physicalQuantityModel);
        }

        private void SetUnit(PhysicalQuantityModel physicalQuantityModel)
        {
            _textView.text =  $"{physicalQuantityModel.abbreviation}";
        }
        
        protected void OnValidate()
        {
            if (_physicalQuantitiesData == null) return;
            if (_physicalQuantitiesData.physicalQuantityModels.ContainsKey(_physicalQuantity))
                SetColor(_physicalQuantitiesData.physicalQuantityModels[_physicalQuantity]);
        }
        
        public void OnEvent(PublisherType publisherType, Component sender, PhysicalQuantityModel param = default)
        {
            SetColor(param);
        }
    }
}