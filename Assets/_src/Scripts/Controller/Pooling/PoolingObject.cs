using System.Collections;
using UnityEngine;

namespace _src.Scripts.Controller.Pooling
{
    public class PoolingObject : MonoBehaviour
    {
        [SerializeField] private PoolingSystem _poolingSystem;
        public float lifetime;
        
        private IEnumerator _disableObject;

        public void SetPoolingObject(PoolingSystem poolingSystem)
        {
            _poolingSystem = poolingSystem;
        }

        private void OnEnable()
        {
            if (_poolingSystem != null)
            {
                lifetime = _poolingSystem.objectsLifetime;
            }
            _disableObject = DisableObject();
            StartCoroutine(_disableObject);
        }

        private void OnDisable()
        {
            if (_poolingSystem._poolQueue.Contains(this)) return;
            _poolingSystem.Enqueue(this);
            StopCoroutine(_disableObject);
        }

        private IEnumerator DisableObject()
        {
            yield return new WaitForSeconds(lifetime);
            _poolingSystem.Enqueue(this);
            gameObject.SetActive(false);
        }
    }
}