using _src.Scripts.Controller.Task;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _src.Scripts.Controller.Systems
{
    public class GameManager : MonoBehaviour
    {
        public TaskCompleteObjective mainTask;
        [SerializeField] private UISystem uiManager;
        [SerializeField] private Window finishWindow;

        private void OnEnable()
        {
            mainTask.OnCompleteEvent += TaskComplete;
        }

        private void OnDisable()
        {
            mainTask.OnCompleteEvent -= TaskComplete;
        }

        private void TaskComplete(IObjective sender, object param)
        {
            uiManager.OpenPopupWindow(finishWindow);
        }

        public void LoadScene(SceneAsset sceneAsset)
        {
            SceneManager.LoadScene(sceneAsset.name);
        }
    }
}