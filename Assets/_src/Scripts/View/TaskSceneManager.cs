using UnityEngine;
using UnityEngine.SceneManagement;

namespace _src.Scripts.View
{
    public class TaskSceneManager : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
