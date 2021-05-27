using System.Collections.Generic;
using _src.Scripts.Controller.Services;
using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    public class EventManager : IService
    {
        private Dictionary<Event_Type, List<IListener>> _gameEvents = new Dictionary<Event_Type, List<IListener>>();

        public void Raise<T>(Event_Type eventType, Component sender, T param = default)
        {
            foreach (var listener in _gameEvents[eventType])
            {
                listener.OnEvent(eventType, sender, param);
            }
        }

        public void RegisterListener(Event_Type eventType, IListener listener)
        {
            if (_gameEvents.TryGetValue(eventType, out var listeners))
            {
                if (!listeners.Contains(listener))
                {
                    listeners.Add(listener);
                    return;
                }
            }

            listeners = new List<IListener>() {listener};
            _gameEvents.Add(eventType, listeners);
        }

        public void UnregisterListener(Event_Type eventType, IListener listener)
        {
            if (_gameEvents.TryGetValue(eventType, out var listeners))
            {
                if (listeners.Contains(listener))
                {
                    listeners.Remove(listener);
                }
            }
        }
        
        // private void RemoveRedundancies()
        // {
        //     Dictionary<Event_Type, List<IListener>> tmpListeners = new Dictionary<Event_Type, List<IListener>>();
        //     foreach (var item in _gameEvents)
        //     {
        //         for (int i = item.Value.Count - 1; i >= 0; i--)
        //         {
        //             if (item.Value[i].Equals(null))
        //             {
        //                 item.Value.RemoveAt(i);
        //             }
        //
        //         }
        //
        //         if (item.Value.Count > 0)
        //             tmpListeners.Add(item.Key, item.Value);
        //         _gameEvents = tmpListeners;
        //     }
        // }
    }

    public enum Event_Type
    {
        RETURN_TO_VARIABLES,
    }
}