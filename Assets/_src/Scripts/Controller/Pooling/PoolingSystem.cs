using System.Collections;
using System.Collections.Generic;
using _src.Scripts.Controller.Interactable;
using UnityEngine;
using UnityEngine.Events;

namespace _src.Scripts.Controller.Pooling
{
    [RequireComponent(typeof(MovementHandler))]
    public class PoolingSystem : MonoBehaviour
    {
        [Header("Fields")]
        public float objectsLifetime;
        [SerializeField] private float _poolingInterval;
        [SerializeField] private int _numberOfInstances;
        [SerializeField] private bool _isDisabled;
        [SerializeField] private Vector3 objectRotation;
        [Header("References")]
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private MovementHandler _movementHandler;

        [SerializeField] private UnityEvent OnObjectEnabled;

        public Queue<PoolingObject> _poolQueue = new Queue<PoolingObject>(new List<PoolingObject>());
        private IEnumerator _pool;

        private void Awake()
        {
            for (int i = 0; i < _numberOfInstances; i++)
            {
                PoolingObject poolingObject = Instantiate(_objectPrefab, transform).AddComponent<PoolingObject>();
                poolingObject.gameObject.SetActive(false);
                _poolQueue.Enqueue(poolingObject);
            }

            _pool = PoolObjects();
            StartCoroutine(_pool);
        }

        private IEnumerator PoolObjects()
        {
            while (!_isDisabled)
            {
                PoolingObject poolingObject = _poolQueue.Dequeue();
                poolingObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(objectRotation));
                poolingObject.gameObject.SetActive(true);
                poolingObject.DisableAfter(objectsLifetime);
                if (poolingObject.TryGetComponent(out MovementComponent movementComponent))
                {
                    movementComponent.Setup(_movementHandler);
                    movementComponent.Move();
                }
                poolingObject.OnDisableEvent += Enqueue;
                OnObjectEnabled.Invoke();
                yield return new WaitForSeconds(_poolingInterval);
            }
        }

        private void Enqueue(PoolingObject poolingObject)
        {
            if (_poolQueue.Contains(poolingObject)) return;
            _poolQueue.Enqueue(poolingObject);
            poolingObject.OnDisableEvent -= Enqueue;
        }
    }
}