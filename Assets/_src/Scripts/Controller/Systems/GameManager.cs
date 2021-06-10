using _src.Scripts.Controller.Task;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _src.Scripts.Controller.Systems
{
    public class GameManager : MonoBehaviour
    {
        public TaskCompleteObserver mainTask;
        [SerializeField] private Window finishWindow;

        private void OnEnable()
        {
            mainTask.OnTriggered += TaskComplete;
        }

        private void OnDisable()
        {
            mainTask.OnTriggered -= TaskComplete;
        }

        private void TaskComplete(IObserver sender, object param)
        {
            finishWindow.gameObject.SetActive(true);
        }

        public void LoadScene(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}