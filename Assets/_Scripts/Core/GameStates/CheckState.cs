using System.Collections.Generic;
using Game.Components;
using Game.Core.Pieces;
using Game.Managers;
using UnityEngine;

namespace Game.Core.GameStates
{
    public class CheckState: GameState
    {
        private Dictionary<Piece, List<Position>> _piecePositions = new Dictionary<Piece, List<Position>>();
        private List<Position> _positionsToMove = new List<Position>();
        private List<Piece> _piecesCanMove = new List<Piece>();

        public CheckState(
            ChessManager chessManager,
            PieceGraveyardManager graveyardManager,
            ChessBoard board,
            Piece whiteKing,
            Piece blackKing
            ) : base(chessManager, graveyardManager, board, whiteKing, blackKing)
        {
        }

        public override void Enter()
        {
            Debug.Log("Check");
            var king = chessManager.Turn == PieceColor.White ? whiteKing : blackKing;
            _positionsToMove = LastPiece.GetMiddlePositionsBetweenThisAndTarget(king.Position, board);
            _positionsToMove.Add(LastPiece.Position);
            FindAllPiecesWhoCanMove(king);
            if (_piecePositions.Count == 0)
            {
                chessManager.ChangeState(chessManager.CheckMateState);
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
                selectedPiece = piece;
                chessManager.ActiveAllGridsInThisPostions(positions);
            }
        }

        protected override void UpdateNextState()
        {
            chessManager.ChangeState(chessManager.NormalState);
        }

        private void FindAllPiecesWhoCanMove(Piece king)
        {
            var pieces = board.GetAllPiecesByColor(chessManager.Turn);
            _piecePositions.Clear();
            var kingMove = King.GetKingMove(king as King, board);
            _piecePositions.Add(king, kingMove);
            foreach (var piece in pieces)
            {
                var positionCanMove = piece.GetAllPosiblePositionsFromList(_positionsToMove, board);
                if (positionCanMove.Count > 0)
                {
                    _piecePositions.Add(piece, positionCanMove);
                    continue;
                }
            }
        }
    }
}