using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    public interface ISubscriber
    {
        void OnPublish<T>(PublisherType publisherType, Component sender, T param = default);
        void OnEnable();
        void OnDisable();
    }
}