using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _src.Scripts.Controller.Task
{
    public class TaskCompleteObserver : Observer
    {
        [SerializeField] private List<Task> tasks = new List<Task>();
        [SerializeField] private bool count = false;

        protected override void OnEnable()
        {
            CountCompleteTasks();
            tasks.ForEach(task =>
            {
                task.OnTriggered += OnInnerTaskComplete;
            });
        }

        protected override void OnDisable()
        {
            tasks.ForEach(task =>
            {
                task.OnTriggered -= OnInnerTaskComplete;
            });
        }

        private void OnInnerTaskComplete(IObserver sender, object param)
        {
            CountCompleteTasks();
            if (tasks.Any(task => task.Status != TaskStatus.COMPLETE))return;
            NotifyObserver(this);
        }

        private void CountCompleteTasks()
        {
            if (count)
            {
                int complete = tasks.Count(task => task.Status == TaskStatus.COMPLETE);
                Task task = GetComponent<Task>();
                task.SetCountText($"{complete}/{tasks.Count}");
            }
        }

        private void OnValidate()
        {
            if (!count) return;
            CountCompleteTasks();
        }
    }
}