using System;
using UnityEngine;

namespace Game.Components
{
    public class BoardGrid : MonoBehaviour, IHoverable, ISelectable
    {
        public event Action<BoardGrid> OnSelectEvent;

        [SerializeField]
        private Position _position;
        
        [SerializeField]
        private MeshRenderer _renderer;
        
        [SerializeField]
        private Collider _collider;

        public Position Position
        {
            get => _position;
        }
        public void OnHoverEnter() { }

        public void OnHoverExit() { }

        public void OnSelect()
        {
            OnSelectEvent?.Invoke(this);
        }

        public void SetActive(bool enable)
        {
            _collider.enabled = enable;
            _renderer.enabled = enable;
        }
    }
}