using System;
using Game.Core;
using UnityEngine;

namespace Game.Components
{
    [RequireComponent(typeof(Collider))]
    public class PieceContainer : MonoBehaviour, IHoverable, ISelectable
    {
        public event Action<Piece> OnSelectEvent;

        [SerializeField]
        private PieceType _pieceType = PieceType.Empty;

        [SerializeField]
        private PieceColor _pieceColor = PieceColor.Empty;

        [SerializeField]
        private Position _initialPosition;

        private Piece _piece;

        private void Awake()
        {
            _piece = PieceFactory.GetPiece(_initialPosition, _pieceType, _pieceColor);
            _piece.Container = this;
        }

        public Piece Piece
        {
            get => _piece;
        }

        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void OnSelect()
        {
            OnSelectEvent?.Invoke(_piece);
        }
    }
}