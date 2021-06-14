using UnityEngine;
using UnityEngine.SceneManagement;

namespace _src.Scripts.View
{
    public class TaskSceneLoader : MonoBehaviour
    {
        [SerializeField] private int sceneNumber;
        
        public void LoadScene()
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
