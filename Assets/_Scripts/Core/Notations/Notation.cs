using Game.Core;
using Game.Core.GameStates;
using Game.Core.Pieces;
using UnityEngine;

namespace Game._Scripts.Core.Notations
{
    public class Notation
    {
        private readonly ChessBoard _board;
        private readonly PieceColor _turn;
        private readonly Piece _lastPiece;
        private readonly PieceData _movedPiece;
        private readonly Piece _deadPiece;
        private readonly GameState _currentState;

        public Notation(
            ChessBoard board,
            PieceColor turn,
            Piece lastPiece,
            PieceData movedPiece,
            Piece deadPiece,
            GameState currentState
            )
        {
            _board = board;
            _turn = turn;
            _lastPiece = lastPiece;
            _movedPiece = movedPiece;
            _deadPiece = deadPiece;
            _currentState = currentState;
        }

        public ChessBoard Board
        {
            get => _board;
        }

        public PieceColor Turn
        {
            get => _turn;
        }

        public Piece LastPiece
        {
            get => _lastPiece;
        }

        public PieceData MovedPiece
        {
            get => _movedPiece;
        }

        public Piece DeadPiece
        {
            get => _deadPiece;
        }

        public GameState CurrentState
        {
            get => _currentState;
        }

        public override string ToString()
        {
            var pieceMoved = _movedPiece.Piece.Sign;
            var from = _movedPiece.FromPosition.ToString();
            var to = _movedPiece.ToPosition.ToString();
            var deadPiece = _deadPiece.Sign;
            var itCapture = _deadPiece is Empty ? "" : "x";
            return pieceMoved + itCapture + from + " " + deadPiece + itCapture + to;
        }
    }

    [System.Serializable]
    public class PieceData
    {
        private Piece _piece;
        private Vector3 _fromTransformPosition;
        private Vector3 _toTransformPosition;
        private Position _fromPosition;
        private Position _toPosition;

        public PieceData(
            Piece piece,
            Vector3 fromTransformPosition,
            Vector3 toTransformPosition,
            Position fromPosition,
            Position toPosition
            )
        {
            _piece = piece;
            _fromTransformPosition = fromTransformPosition;
            _toTransformPosition = toTransformPosition;
            _fromPosition = fromPosition;
            _toPosition = toPosition;
        }

        public Piece Piece
        {
            get => _piece;
        }

        public Vector3 FromTransformPosition
        {
            get => _fromTransformPosition;
        }

        public Vector3 ToTransformPosition
        {
            get => _toTransformPosition;
        }

        public Position FromPosition
        {
            get => _fromPosition;
        }

        public Position ToPosition
        {
            get => _toPosition;
        }
    }
}