using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _src.Scripts.Controller.Task
{
    [Serializable]
    public class Task : MonoBehaviour, IObserver
    {
        [TextArea]
        [SerializeField] private string _description;
        [SerializeField] private TaskStatus _status;
        [SerializeField] private Observer _observer;
        
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Toggle _completeToggle;

        public event IObserver.Trigger OnTriggered;
        
        public void NotifyObserver(object param = default)
        {
            OnTriggered?.Invoke(this);
        }

        public TaskStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                SetView(value);
            }
        }

        protected void OnEnable()
        {
            _observer.OnTriggered += Complete;
        }

        protected void OnDisable()
        {
            _observer.OnTriggered -= Complete;
        }

        private void Complete(IObserver sender, object param)
        {
            SetView(TaskStatus.COMPLETE);
            NotifyObserver(this);
            _observer.OnTriggered -= Complete;
        }

        private void OnValidate()
        {
            SetView(Status);
        }

        private void SetView(TaskStatus newStatus)
        {
            _status = newStatus;
            if (_descriptionText != null) _descriptionText.text = _description;
            if (_completeToggle != null) _completeToggle.isOn = Status == TaskStatus.COMPLETE;
        }

        public void SetCountText(string countText)
        {
            _descriptionText.text = $"{_description} {countText}";
        }
    }
    
    public enum TaskStatus
    {
        WAITING,
        COMPLETE
    }
}