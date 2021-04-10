using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SampleScenes.Scripts
{
    public class LevelReset :MonoBehaviour , IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData data)
        {
            // reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}