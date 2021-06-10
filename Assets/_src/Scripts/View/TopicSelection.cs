using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TopicSelection : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private List<Transform> _topicIslands;

    public void OnSelectTopic(int topicNumber)
    {
        _camera.m_LookAt = _topicIslands[topicNumber];
        _camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = topicNumber;
    }
}
