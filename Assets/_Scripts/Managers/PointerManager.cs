using System;
using Game.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Managers
{
    public class PointerManager : MonoBehaviour
    {
        [SerializeField]
        private ChessManager _chessManager;

        [SerializeField]
        private Material _highlightMaterial;

        [SerializeField]
        private Camera _camera;
        
        [SerializeField]
        private LayerMask _layerWhite;

        [SerializeField]
        private LayerMask _layerBlack;

        private Vector2 _pointerScreenPosition;
        private Transform _pointing;
        private Material _defaultMaterial;

        private LayerMask CurrentLayer
        {
            get
            {
                return _chessManager.Turn == PieceColor.White ? _layerWhite : _layerBlack;
            }
        }

        private void LateUpdate()
        {
            Ray ray = _camera.ScreenPointToRay(_pointerScreenPosition);
            RaycastHit hitInfo;
            Debug.DrawRay(_camera.transform.position, ray.direction, Color.red);
            if (Physics.Raycast(ray, out hitInfo, 1000f, CurrentLayer))
            {
                var pointing = hitInfo.transform;
                if (pointing.GetComponent<IHoverable>() != null)
                {
                    ChangePointingObject(pointing);
                }
                else
                {
                    ChangePointingObject();
                }
            }
            else
            {
                ChangePointingObject();
            }
        }

        public void Point(InputAction.CallbackContext context)
        {
            _pointerScreenPosition = context.ReadValue<Vector2>();
        }
        
        public void Select(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SelectPointing();
            }
        }

        private void ChangePointingObject(Transform pointing = null)
        {
            if (_pointing == pointing) return;

            if (_pointing != null)
            {
                _pointing.GetComponent<IHoverable>()?.OnHoverExit();
                SetBackToDefaultMaterial(_pointing);
            }
            _pointing = pointing;
            if (_pointing == null) return;

            _pointing.GetComponent<IHoverable>().OnHoverEnter();
            SetHoverMaterial(_pointing);
        }

        private void SetHoverMaterial(Transform pointing)
        {
            var renderer = pointing.GetComponentInChildren<MeshRenderer>();
            _defaultMaterial = renderer.material;
            renderer.material = _highlightMaterial;
        }

        private void SetBackToDefaultMaterial(Transform pointing)
        {
            var renderer = pointing.GetComponentInChildren<MeshRenderer>();
            renderer.material = _defaultMaterial;
        }

        private void SelectPointing()
        {
            if (_pointing == null) return;
            _pointing.GetComponent<ISelectable>()?.OnSelect();
        }
    }
}
