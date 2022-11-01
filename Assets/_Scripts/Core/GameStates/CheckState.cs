using Game.Components;
using Game.Managers;

namespace Game.Core.GameStates
{
    public class CheckState: GameState
    {
        public CheckState(ChessManager chessManager, PieceGraveyardManager graveyardManager, ChessBoard board, Piece whiteKing, Piece blackKing) : base(chessManager, graveyardManager, board, whiteKing, blackKing)
        {
        }

        public override void Enter()
        {
            FindAllPiecesWhoCanMove();
        }

        public override void Exit()
        {
            
        }

        public override void SelectPiece(Piece piece)
        {
            
        }

        protected override void UpdateNextState()
        {
            
        }

        private void FindAllPiecesWhoCanMove()
        {
            // board
        }
    }
}