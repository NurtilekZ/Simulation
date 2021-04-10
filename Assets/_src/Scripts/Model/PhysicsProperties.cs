using System.Collections.Generic;
using UnityEngine;

namespace _src.Scripts.Model
{
    [CreateAssetMenu]
    public class PhysicsProperties : ScriptableObject
    {
        [SerializeField] private List<PhysicsPropertyModel> physicsProperties = new List<PhysicsPropertyModel>();
        public readonly Dictionary<PhysicsProperty, string> Units = new Dictionary<PhysicsProperty, string>();
        public readonly Dictionary<PhysicsProperty, Color> Colors = new Dictionary<PhysicsProperty, Color>();

        private void OnValidate()
        {
            if (physicsProperties.Count <= 0) return;
            foreach (PhysicsPropertyModel property in physicsProperties)
            {
                if (!Units.ContainsKey(property.name))
                {
                    Units.Add(property.name, property.unit);
                }
                else
                {
                    Units[property.name] = property.unit;
                }
                    
                if (!Colors.ContainsKey(property.name))
                {
                    Colors.Add(property.name, property.color);
                }
                else
                {
                    Colors[property.name] = property.color;
                }
            }
        }
    }
}