using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Game.Components.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
