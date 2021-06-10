using UnityEngine;

namespace _src.Scripts.Controller.Interactables
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] public bool isStaticObstacle = false;
        
        private void OnCollisionEnter(Collision other)
        {
            if (isStaticObstacle) return;
            if (other.gameObject.GetComponent<Obstacle>())
            {
                gameObject.SetActive(false);
            }
        }
    }
}