using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    public interface IListener
    {
        void OnEvent<T>(Event_Type eventType, Component sender, T param = default);
        void OnEnable();
        void OnDisable();
    }
}