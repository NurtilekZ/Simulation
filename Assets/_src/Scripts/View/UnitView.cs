using _src.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _src.Scripts.View
{
    public class UnitView : MonoBehaviour
    {
        
        [SerializeField] private Image imageView;

        [SerializeField] protected TextMeshProUGUI textView;
        [SerializeField] protected PhysicsProperties physicsProperties;
        [SerializeField] protected PhysicsProperty physicsProperty;
        public PhysicsProperty Property
        {
            get => physicsProperty;
            set
            {
                physicsProperty = value;
                SetColor();
            }
        }
        
        protected virtual void SetColor()
        {
            textView.text =  $"{physicsProperties.Units[Property]}";
            imageView.color = physicsProperties.Colors[Property];
        }
        
        protected virtual void OnValidate()
        {
            SetColor();
        }
    }

}