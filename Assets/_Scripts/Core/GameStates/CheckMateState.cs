using UnityEngine;

namespace Game.Core.GameStates
{
    public class CheckMateState: GameState
    {
        public override void Enter()
        {
            Debug.Log("Check Mate");
            var winner = chessManager.Turn == PieceColor.Black ? PieceColor.White : PieceColor.Black;
            historyManager.SetLastCheckpointCheckToMate(this);
        }

        public override void Exit()
        {
            
        }

        protected override void SelectPieceState(Piece piece)
        {
            return;
        }

        protected override void UpdateNextState()
        {
            return;
        }
    }
}