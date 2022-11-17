using Game.Components;
using Game.Managers;
using UnityEngine;

namespace Game.Core.GameStates
{
    public class NormalState: GameState
    {
        public NormalState(ChessManager chessManager, PieceGraveyardManager graveyardManager, ChessBoard board, Piece whiteKing, Piece blackKing) : base(chessManager, graveyardManager, board, whiteKing, blackKing)
        {
        }

        protected override void SelectPieceState(Piece piece)
        {
            selectedPiece = piece;
            var allMovePositions = piece.GetAllMovePositions(board);
            chessManager.DeactiveAllActivedGrids();
            chessManager.ActiveAllGridsInThisPostions(allMovePositions);
        }
        public override void Enter() {Debug.Log("Normal"); }

        public override void Exit() { }

        protected override void UpdateNextState()
        {
            var currentKing = chessManager.Turn == PieceColor.Black ? blackKing : whiteKing;
            if (LastPiece.CanMove(currentKing.Position, board))
            {
                chessManager.ChangeState(chessManager.CheckState);
            }
        }
    }
}