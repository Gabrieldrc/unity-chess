using System.Collections.Generic;
using Game.Core;
using Game.Core.Pieces;
using UnityEngine;

namespace Game.Managers
{
    public class PieceGraveyardManager : MonoBehaviour
    {
        [SerializeField, Tooltip("It has to have 16 positions")]
        private List<Transform> _blackPiecesGraveyardPositions;

        [SerializeField, Tooltip("It has to have 16 positions")]
        private List<Transform> _whitePiecesGraveyardPositions;

        private List<Piece> _blackPieces;
        private List<Piece> _whitePieces;

        private void Awake()
        {
            _blackPieces = new List<Piece>();
            _whitePieces = new List<Piece>();
            if (_blackPiecesGraveyardPositions.Count != 16)
            {
                Debug.LogError($"it has to have 16 positions in the black pieces graveyard positions, instead there is {_blackPiecesGraveyardPositions.Count}");
            }
            if (_whitePiecesGraveyardPositions.Count != 16)
            {
                Debug.LogError($"it has to have 16 positions in the white pieces graveyard positions, instead there is {_whitePiecesGraveyardPositions.Count}");
            }
        }

        public void AddPieceToGraveyard(Piece piece)
        {
            if (piece is Empty)
            {
                return;
            }

            var graveyard = RegisterPieceToGraveyard(piece);
            MovePieceToGraveyard(piece, graveyard);
        }

        private Vector3 RegisterPieceToGraveyard(Piece piece)
        {
            var index = -1;
            Vector3 cementaryPosition;
            if (piece.Color == PieceColor.Black)
            {
                _blackPieces.Add(piece);
                index = _blackPieces.FindIndex(p => p == piece);
                cementaryPosition = _blackPiecesGraveyardPositions[index].transform.position;
            }
            else
            {
                _whitePieces.Add(piece);
                index = _whitePieces.FindIndex(p => p == piece);
                cementaryPosition = _whitePiecesGraveyardPositions[index].transform.position;
            }

            return cementaryPosition;
        }

        private void MovePieceToGraveyard(Piece piece, Vector3 graveyardPosition)
        {
            piece.Container.transform.position = graveyardPosition;
        }
    }
}
