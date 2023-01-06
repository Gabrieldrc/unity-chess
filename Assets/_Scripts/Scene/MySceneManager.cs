using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scene
{
    public class MySceneManager : MonoBehaviour
    {

        public static void LoadScene(SceneSO sceneData)
        {
            SceneManager.LoadScene(sceneData.Name, sceneData.LoadMode);
        }
        
        public static AsyncOperation LoadSceneAsync(SceneSO sceneData)
        {
            return SceneManager.LoadSceneAsync(sceneData.Name, sceneData.LoadMode);
        }
    }
}
