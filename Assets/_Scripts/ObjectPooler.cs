using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPooler : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        private GameObject _pooledObject;

        [SerializeField] private int _pooledAmount = 1;
        [SerializeField] private bool _willGrow = true;
        [SerializeField] private List<GameObject> _pooledObjects;
        [SerializeField] private Transform _parent;

        private void Start()
        {
            _pooledObjects = new List<GameObject>();
            for (var i = 0; i < _pooledAmount; i++) CreateObject();
        }

        public GameObject GetPooledObject()
        {
            for (var i = 0; i < _pooledObjects.Count; i++)
                if (!_pooledObjects[i].activeSelf)
                    return _pooledObjects[i];

            if (_willGrow) return CreateObject();

            return null;
        }

        private GameObject CreateObject()
        {
            GameObject obj;
            if (_parent != null)
                obj = Instantiate(_pooledObject, _parent);
            else
                obj = Instantiate(_pooledObject);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
            return obj;
        }
    }
}