using System.Collections;
using System.Collections.Generic;
using _src.Scripts.Controller.Interactables;
using UnityEngine;

namespace _src.Scripts.Controller.Pooling
{
    public class PoolingSystem : MonoBehaviour
    {
        public float objectsLifetime;
        [SerializeField] private float _intervalSeconds;
        [SerializeField] private int _numberOfInstances;
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private bool _isStopped;
        [SerializeField] private Vector3 objectRotation;

        public Queue<PoolingObject> _poolQueue = new Queue<PoolingObject>(new List<PoolingObject>());
        private IEnumerator _pool;

        private void Awake()
        {
            for (int i = 0; i < _numberOfInstances; i++)
            {
                PoolingObject poolingObject = Instantiate(_objectPrefab, transform).AddComponent<PoolingObject>();
                poolingObject.SetPoolingObject(this);
                poolingObject.gameObject.SetActive(false);
                _poolQueue.Enqueue(poolingObject);
            }

            _pool = PoolObjects();
            StartCoroutine(_pool);
        }

        private IEnumerator PoolObjects()
        {
            while (!_isStopped)
            {
                PoolingObject poolingObject = _poolQueue.Dequeue();
                poolingObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(objectRotation));
                poolingObject.lifetime = objectsLifetime;
                poolingObject.gameObject.SetActive(true);
                poolingObject.GetComponent<MovementComponent>().Move();
                yield return new WaitForSeconds(_intervalSeconds);
            }
        }

        public void Enqueue(PoolingObject poolingObject)
        {
            _poolQueue.Enqueue(poolingObject);
        }
    }
}