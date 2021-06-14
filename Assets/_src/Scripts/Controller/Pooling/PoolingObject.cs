using System;
using System.Collections;
using UnityEngine;

namespace _src.Scripts.Controller.Pooling
{
    public class PoolingObject : MonoBehaviour
    {
        public event Action<PoolingObject> OnDisableEvent;
        private IEnumerator _disable;

        public void DisableAfter(float lifetime)
        {
            _disable = Disable(lifetime);
            StartCoroutine(_disable);
        }

        private void OnDisable()
        {
            OnDisableEvent?.Invoke(this);
            StopCoroutine(_disable);
        }

        private IEnumerator Disable(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            gameObject.SetActive(false);
        }
    }
}