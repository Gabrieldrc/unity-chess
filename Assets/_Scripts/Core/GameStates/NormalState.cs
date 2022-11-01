using Game.Components;
using Game.Managers;

namespace Game.Core.GameStates
{
    public class NormalState: GameState
    {
        public NormalState(ChessManager chessManager, PieceGraveyardManager graveyardManager, ChessBoard board, Piece whiteKing, Piece blackKing) : base(chessManager, graveyardManager, board, whiteKing, blackKing)
        {
        }

        public override void Enter() { }

        public override void Exit() { }

        protected override void UpdateNextState()
        {
            var currentKing = chessManager.Turn == PieceColor.Black ? blackKing : whiteKing;
            if (lastPiece.CanMove(currentKing.Position, board))
            {
                chessManager.NextState = chessManager.CheckState;
            }
        }
    }
}