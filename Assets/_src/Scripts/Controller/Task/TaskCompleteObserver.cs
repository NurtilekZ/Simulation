using System.Collections.Generic;
using System.Linq;

namespace _src.Scripts.Controller.Task
{
    public class TaskCompleteObserver : Observer
    {
        public List<Task> tasks = new List<Task>();

        protected override void OnEnable()
        {
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
            if (tasks.Any(task => task.Status == TaskStatus.WAITING))return;
            NotifyObserver(this);
        }
    }
}