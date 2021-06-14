using UnityEngine;

namespace _src.Scripts.Controller.Interactable
{
    public class MovementHandler : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] public float velocity;
        [SerializeField] public Vector3 direction;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, direction * 2);
        }
    }
}