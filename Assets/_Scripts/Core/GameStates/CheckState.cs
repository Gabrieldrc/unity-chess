using System.Collections.Generic;
using Game.Core.Pieces;
using UnityEngine;

namespace Game.Core.GameStates
{
    public class CheckState: GameState
    {
        [SerializeField]
        private GameState _checkMateState;

        [SerializeField]
        private GameState _normalState;

        private Dictionary<Piece, List<Position>> _piecePositions = new Dictionary<Piece, List<Position>>();
        private List<Position> _positionsToMove = new List<Position>();
        private List<Piece> _piecesCanMove = new List<Piece>();

        public override void Enter()
        {
            Debug.Log("Check");
            var king = chessManager.Turn == PieceColor.White ? WhiteKing : BlackKing;
            _positionsToMove = LastPiece.GetMiddlePositionsBetweenThisAndTarget(king.Position, Board);
            _positionsToMove.Add(LastPiece.Position);
            FindAllPiecesWhoCanMove(king);
            if (_piecePositions.Count == 0)
            {
                chessManager.ChangeState(_checkMateState);
            }
        }

        public override void Exit()
        {
            
        }

        protected override void SelectPieceState(Piece piece)
        {
            List<Position> positions;
            if (_piecePositions.TryGetValue(piece, out positions))
            {
                Debug.Log($"{piece.Sign} -> {positions.Count}");
                selectedPiece = piece;
                chessManager.ActiveAllGridsInThisPostions(positions);
            }
        }

        protected override void UpdateNextState()
        {
            chessManager.ChangeState(_normalState);
        }

        private void FindAllPiecesWhoCanMove(Piece king)
        {
            var pieces = Board.GetAllPiecesByColor(chessManager.Turn);
            _piecePositions.Clear();
            var kingMove = King.GetKingMove(king as King, Board);
            _piecePositions.Add(king, kingMove);
            foreach (var piece in pieces)
            {
                if (piece is King)
                {
                    continue;
                }
                var positionCanMove = piece.GetAllPosiblePositionsFromList(_positionsToMove, Board);
                if (positionCanMove.Count > 0)
                {
                    _piecePositions.Add(piece, positionCanMove);
                    continue;
                }
            }
        }
    }
}