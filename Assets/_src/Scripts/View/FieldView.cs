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
            set
            {
                _physicalQuantity = value;
                if (_physicalQuantitiesData.PhysicalQuantityModels.ContainsKey(_physicalQuantity))
                    SetColorAndUnit(_physicalQuantitiesData.PhysicalQuantityModels[_physicalQuantity]);
            }
        }

        protected virtual void SetColorAndUnit(PhysicalQuantityModel physicsPropertyModel)
        {
            _imageView.color = physicsPropertyModel.color;
            _textView.text =  $"{physicsPropertyModel.abbreviation}";
        }
        
        protected void OnValidate()
        {
            if (_physicalQuantitiesData == null) return;
            if (_physicalQuantitiesData.PhysicalQuantityModels.ContainsKey(_physicalQuantity))
                SetColorAndUnit(_physicalQuantitiesData.PhysicalQuantityModels[_physicalQuantity]);
        }
        
        public void OnEvent(Event_Type eventType, Component sender, PhysicalQuantityModel param = default)
        {
            SetColorAndUnit(param);
        }
    }
}