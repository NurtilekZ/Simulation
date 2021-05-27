using System.Collections.Generic;
using UnityEngine;

namespace _src.Scripts.Controller.Systems
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private List<Window> _popupWindows = new List<Window>();
        
        public void OpenPopupWindow(Window window)
        {
            if (_popupWindows.Contains(window))
            {
                window.Open();
            }
        }
    }
}