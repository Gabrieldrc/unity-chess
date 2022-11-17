using Game.Components;
using Game.Managers;
using UnityEngine;

namespace Game.Core.GameStates
{
    public class CheckMateState: GameState
    {
        public CheckMateState(
            ChessManager chessManager,
            PieceGraveyardManager graveyardManager,
            ChessBoard board,
            Piece whiteKing, Piece blackKing
            ) : base(chessManager, graveyardManager, board, whiteKing, blackKing)
        {
        }

        public override void Enter()
        {
            Debug.Log("Check Mate");
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