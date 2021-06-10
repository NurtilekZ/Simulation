using System;
using UnityEngine;

namespace _src.Scripts.Model
{
    [Serializable]
    public class PhysicalQuantityModel
    {
        public PhysicalQuantity name;
        public string abbreviation;
        public string unit;
        public Color color;
    }
}