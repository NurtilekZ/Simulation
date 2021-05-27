using UnityEngine;

namespace _src.Scripts.Controller.Task
{
    [System.Serializable]
    public abstract class Objective : MonoBehaviour, IObjective
    {
        public event IObjective.Complete OnCompleteEvent;

        public virtual void CompleteObjective(object param = default)
        {
            OnCompleteEvent?.Invoke(this, param);
        }
    }
}