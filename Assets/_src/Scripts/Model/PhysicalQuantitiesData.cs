using System;
using System.Collections.Generic;
using UnityEngine;

namespace _src.Scripts.Model
{
    [CreateAssetMenu]
    [Serializable]
    public class PhysicalQuantitiesData : ScriptableObject
    {
        [SerializeField] private List<PhysicalQuantityModel> physicsProperties = new List<PhysicalQuantityModel>();
        
        public Dictionary<PhysicalQuantity, PhysicalQuantityModel> physicalQuantityModels = new Dictionary<PhysicalQuantity, PhysicalQuantityModel>();

        private void Awake()
        {
            FillDictionary();
        }

        private void OnValidate()
        {
            FillDictionary();
        }

        private void FillDictionary()
        {
            if (physicsProperties.Count <= 0) return;
            foreach (PhysicalQuantityModel property in physicsProperties)
            {
                if (!physicalQuantityModels.ContainsKey(property.name))
                {
                    physicalQuantityModels.Add(property.name, property);
                }
                else if (physicalQuantityModels[property.name] == property) continue;

                physicalQuantityModels[property.name] = property;
            }
        }
    }
}