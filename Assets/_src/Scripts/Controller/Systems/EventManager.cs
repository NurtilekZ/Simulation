using System.Collections.Generic;
using _src.Scripts.Controller.Services;
using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    public class EventManager : IService
    {
        private Dictionary<PublisherType, List<ISubscriber>> _pubSubsDictionary = new Dictionary<PublisherType, List<ISubscriber>>();

        public void Publish<T>(PublisherType publisherType, Component sender, T param = default)
        {
            foreach (var subscriber in _pubSubsDictionary[publisherType])
            {
                subscriber.OnPublish(publisherType, sender, param);
            }
        }

        public void RegisterListener(PublisherType publisherType, ISubscriber subscriber)
        {
            if (_pubSubsDictionary.TryGetValue(publisherType, out var subscribers))
            {
                if (!subscribers.Contains(subscriber))
                {
                    subscribers.Add(subscriber);
                    return;
                }
            }

            subscribers = new List<ISubscriber>() {subscriber};
            _pubSubsDictionary.Add(publisherType, subscribers);
        }

        public void UnregisterListener(PublisherType publisherType, ISubscriber subscriber)
        {
            if (!_pubSubsDictionary.TryGetValue(publisherType, out var subscribers)) return;
            if (subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }
        }
    }

    public enum PublisherType
    {
        RETURN_TO_VARIABLES,
    }
}