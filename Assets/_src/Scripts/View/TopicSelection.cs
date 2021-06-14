using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace _src.Scripts.View
{
    public class TopicSelection : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private List<Transform> _topicIslands;

        private void Awake()
        {
            Application.targetFrameRate = 300;
        }

        public void OnSelectTopic(int topicNumber)
        {
            _camera.m_LookAt = _topicIslands[topicNumber];
            _camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = topicNumber;
        }
    }
}
