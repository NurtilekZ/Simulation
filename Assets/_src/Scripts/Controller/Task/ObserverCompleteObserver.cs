using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _src.Scripts.Controller.Task
{
    public class ObserverCompleteObserver : Observer
    {
        [SerializeField] private List<Observer> tasks = new List<Observer>();
        [SerializeField] private bool count = false;

        protected override void OnEnable()
        {
            CountCompleteTasks();
            tasks.ForEach(observer =>
            {
                observer.OnTriggered += OnInnerObserverComplete;
            });
        }

        protected override void OnDisable()
        {
            tasks.ForEach(observer =>
            {
                observer.OnTriggered -= OnInnerObserverComplete;
            });
        }

        private void OnInnerObserverComplete(IObserver sender, object param)
        {
            CountCompleteTasks();
            if (!tasks.All(observer => observer.isNotified))return;
            NotifyObserver(this);
        }

        private void CountCompleteTasks()
        {
            if (count)
            {
                int complete = tasks.Count(observer => observer.isNotified);
                Task task = GetComponent<Task>();
                task.SetCountText($"{complete}/{tasks.Count}");
                
            }
        }

        private void OnValidate()
        {
            CountCompleteTasks();
        }
    }
}