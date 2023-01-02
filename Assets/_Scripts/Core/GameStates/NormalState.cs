using UnityEngine;

namespace Game.Core.GameStates
{
    public class NormalState: GameState
    {
        [SerializeField]
        private GameState _checkState;

        protected override void SelectPieceState(Piece piece)
        {
            selectedPiece = piece;
            var allMovePositions = piece.GetAllMovePositions(Board);
            chessManager.DeactiveAllActivedGrids();
            chessManager.ActiveAllGridsInThisPostions(allMovePositions);
        }

        public override void Enter()
        {
            Debug.Log("Normal");
        }

        public override void Exit() { }

        protected override void UpdateNextState()
        {
            var currentKing = chessManager.Turn == PieceColor.Black ? BlackKing : WhiteKing;
            if (selectedPiece.CanMove(currentKing.Position, Board))
            {
                _nextState = _checkState;
            }
        }
    }
}