using System;
using TMPro;
using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    [Serializable]
    public class Window : MonoBehaviour
    {
        public string title;
        public TextMeshProUGUI titleText;
        [SerializeField] private RectTransform rectTransform;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        private void OnValidate()
        {
            if (titleText != null)
                titleText.text = title;
        }
    }
}