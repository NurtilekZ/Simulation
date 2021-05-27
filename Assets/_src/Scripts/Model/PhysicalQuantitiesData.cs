using System.Collections.Generic;
using UnityEngine;

namespace _src.Scripts.Model
{
    [CreateAssetMenu]
    public class PhysicalQuantitiesData : ScriptableObject
    {
        [SerializeField] private List<PhysicalQuantityModel> physicsProperties = new List<PhysicalQuantityModel>();
        
        public Dictionary<PhysicalQuantity, PhysicalQuantityModel> PhysicalQuantityModels = new Dictionary<PhysicalQuantity, PhysicalQuantityModel>();

        private void OnValidate()
        {
            if (physicsProperties.Count <= 0) return;
            foreach (PhysicalQuantityModel property in physicsProperties)
            {
                if (!PhysicalQuantityModels.ContainsKey(property.name))
                {
                    PhysicalQuantityModels.Add(property.name, property);
                }
                else if (PhysicalQuantityModels[property.name] == property) continue;
                    PhysicalQuantityModels[property.name] = property;
            }
        }
    }
}