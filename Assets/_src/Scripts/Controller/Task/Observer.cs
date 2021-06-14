using System;
using UnityEngine;

namespace _src.Scripts.Controller.Task
{
    [Serializable]
    public abstract class Observer : MonoBehaviour, IObserver
    {
        public event IObserver.Trigger OnTriggered;
        public bool isNotified;
        public virtual void NotifyObserver(object param = default)
        {
            isNotified = true;
            OnTriggered?.Invoke(this, param);
        }

        protected abstract void OnEnable();

        protected abstract void OnDisable();
    }
}