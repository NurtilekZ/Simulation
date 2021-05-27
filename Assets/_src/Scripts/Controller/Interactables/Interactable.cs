using _src.Scripts.View;
using UnityEngine;

namespace _src.Scripts.Controller.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public DropArea dropArea;
        public Camera cameraCache;
        private Vector3 _initialPosition;

        // Start is called before the first frame update
        void Start()
        {
            _initialPosition = transform.position;
            if (dropArea == null)
            {
                dropArea.transform.position = cameraCache.WorldToScreenPoint(transform.position + new Vector3(0,1.5f,0));
            }
        }

        private void OnValidate()
        {
            if (dropArea != null && cameraCache != null)
            {
                dropArea.transform.position = cameraCache.WorldToScreenPoint(transform.position + new Vector3(0,1.5f,0));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.GetComponent<Obstacle>()) return;
            GetComponent<Rigidbody>().Sleep();
            transform.position = _initialPosition;
        }
    }
}