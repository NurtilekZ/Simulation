using System.Collections.Generic;
using System.Linq;

namespace _src.Scripts.Controller.Task
{
    public class TaskCompleteObjective : Objective
    {
        public List<Task> tasks = new List<Task>();

        private void OnEnable()
        {
            tasks.ForEach(task =>
            {
                task.OnCompleteEvent += OnInnerTaskComplete;
            });
        }

        private void OnDisable()
        {
            tasks.ForEach(task =>
            {
                task.OnCompleteEvent -= OnInnerTaskComplete;
            });
        }

        private void OnInnerTaskComplete(IObjective sender, object param)
        {
            if (tasks.Any(task => task.Status == TaskStatus.WAITING))return;
            CompleteObjective(this);
        }
    }
}