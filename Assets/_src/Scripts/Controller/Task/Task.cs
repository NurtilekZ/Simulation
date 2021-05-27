using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _src.Scripts.Controller.Task
{
    [Serializable]
    public class Task : MonoBehaviour, IObjective
    {
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private TaskStatus status;
        [SerializeField] private Objective objective;
        
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Toggle _completeToggle;

        public event IObjective.Complete OnCompleteEvent;
        
        public void CompleteObjective(object param = default)
        {
            OnCompleteEvent?.Invoke(this);
        }

        public TaskStatus Status
        {
            get => status;
            set
            {
                status = value;
                SetView(value);
            }
        }

        private void OnEnable()
        {
            objective.OnCompleteEvent += OnComplete;
        }
        
        private void OnDisable()
        {
            objective.OnCompleteEvent -= OnComplete;
        }

        private void OnComplete(IObjective sender, object param)
        {
            SetView(TaskStatus.COMPLETE);
            objective.OnCompleteEvent -= OnComplete;
            CompleteObjective(this);
        }

        private void OnValidate()
        {
            SetView(Status);
        }

        private void SetView(TaskStatus newStatus)
        {
            status = newStatus;
            if (_descriptionText != null) _descriptionText.text = description;
            if (_completeToggle != null) _completeToggle.isOn = Status == TaskStatus.COMPLETE;
        }
    }
    
    public enum TaskStatus
    {
        WAITING,
        COMPLETE
    }
}