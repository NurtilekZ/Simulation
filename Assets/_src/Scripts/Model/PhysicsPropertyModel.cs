using System;
using UnityEngine;

namespace _src.Scripts.Model
{
    [Serializable]
    public class PhysicsPropertyModel
    {
        public PhysicsProperty name;
        public string unit;
        public Color color;
    }
    
    public enum PhysicsProperty
    {
        Velocity,
        Distance,
        Acceleration,
        Time,
        Mass
    }
}