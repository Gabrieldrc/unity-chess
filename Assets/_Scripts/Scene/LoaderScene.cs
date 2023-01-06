using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scene
{
    public class LoaderScene : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadScreen;
        
        [SerializeField]
        private Image _loadSlider;

        public void LoadScene(SceneSO sceneData)
        {
            StartCoroutine(LoadSceneMode(sceneData));
        }

        private IEnumerator LoadSceneMode(SceneSO sceneSo)
        {
            var operation = MySceneManager.LoadSceneAsync(sceneSo);
            _loadScreen.SetActive(true);
            yield return new WaitForSecondsRealtime(1);
            while (!operation.isDone)
            {
                var loadAmount = Mathf.Clamp01(operation.progress / 0.9f);
                _loadSlider.fillAmount = loadAmount;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }
}