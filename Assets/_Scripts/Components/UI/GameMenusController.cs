using System;
using UnityEngine;

namespace Game.Components.UI
{
    public class GameMenusController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _notationPanel;

        [SerializeField]
        private Camera _mainCamera;
        
        [SerializeField]
        private Transform _finalCameraPositionTranform;

        [Header("Config")]
        [SerializeField, Range(0.1f, 5f)]
        private float _speed = 1;

        private Vector2 _originalPanelPosition;
        private Vector2 _finalPanelPosition;
        private Vector3 _originalCameraPosition;
        private Vector3 _currentCameraDestination;
        private Vector3 _currentPanelDestination;
        private bool _isOpen = false;

        private void Start()
        {
            _originalPanelPosition = _notationPanel.anchoredPosition;
            _originalCameraPosition = _mainCamera.transform.position;
            _finalPanelPosition = new Vector2(0, _originalPanelPosition.y);
            _currentCameraDestination = _originalCameraPosition;
            _currentPanelDestination = _originalPanelPosition;
        }

        private void LateUpdate()
        {
            if (!Mathf.Approximately(_mainCamera.transform.position.x, _currentCameraDestination.x))
            {
                _mainCamera.transform.position =
                    Vector3.Lerp(_mainCamera.transform.position, _currentCameraDestination, Time.deltaTime * _speed);
            }
            if (!Mathf.Approximately(_notationPanel.anchoredPosition.x, _currentPanelDestination.x))
            {
                _notationPanel.anchoredPosition =
                Vector2.Lerp(_notationPanel.anchoredPosition, _currentPanelDestination, Time.deltaTime * _speed);
            }
        }

        public void OpenNotationPanel()
        {
            if (_isOpen)
            {
                _currentPanelDestination = _originalPanelPosition;
                _currentCameraDestination = _originalCameraPosition;
            }
            else
            {
                _currentPanelDestination = _finalPanelPosition;
                _currentCameraDestination = _finalCameraPositionTranform.position;
            }
            _isOpen = !_isOpen;
        }
    }
}
