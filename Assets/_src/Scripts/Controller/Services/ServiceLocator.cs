using System;
using System.Collections.Generic;
using _src.Scripts.Controller.Systems;
using UnityEngine;

namespace _src.Scripts.Controller.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<object, object> _services;
        
        private static ServiceLocator _Current;
        public static ServiceLocator Current
        {
            get => _Current ??= new ServiceLocator();
            private set => _Current = value;
        }

        private ServiceLocator()
        {
            _services = new Dictionary<object, object>
                {{typeof(EventManager), new EventManager()}};
        }
        
        public T GetService<T>()
        {
            if (_services.ContainsKey(typeof(T))) return (T) _services[typeof(T)];

            Debug.LogError($"{typeof(T)} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }
        
        public void RegisterService<T>(T service)
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                _services.Add(typeof(T),service);
            }
        }
        
        public void UnregisterService<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                _services.Remove(typeof(T));
            }
        }
    }
}