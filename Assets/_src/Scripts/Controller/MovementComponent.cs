using System;
using System.Windows.Input;
using UnityEngine;

namespace _src.Scripts.Controller
{
    public class MovementComponent : MonoBehaviour, ICommand
    {
        public Vector2 velocity;
        public Vector3 direction;
        public Rigidbody rb;
        public Vector3 initialPosition;

        private void Awake()
        {
            initialPosition = transform.position;
        }

        public void Reset()
        {
            transform.position = initialPosition;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            rb.AddForce(direction * velocity, ForceMode.VelocityChange);
        }

        public event EventHandler CanExecuteChanged;
    }
}