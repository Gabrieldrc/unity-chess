using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scene
{
    [CreateAssetMenu(fileName = "Scene", menuName = "SO/Scene/new scene", order = 0)]
    public class SceneSO : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private LoadSceneMode _loadSceneMode;

        public string Name { get => _name; }
        public LoadSceneMode LoadMode { get => _loadSceneMode; }
    }
}