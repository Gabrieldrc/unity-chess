using Game.Components;
using Game.Managers;

namespace Game.Core.GameStates
{
    public abstract class GameState
    {
        protected ChessManager chessManager;
        protected PieceGraveyardManager graveyardManager;
        protected ChessBoard board;
        protected Piece selectedPiece;
        public Piece LastPiece { get; set; } = null;
        protected Piece whiteKing;
        protected Piece blackKing;
        protected GameState(ChessManager chessManager, PieceGraveyardManager graveyardManager, ChessBoard board, Piece whiteKing, Piece blackKing)
        {
            this.chessManager = chessManager;
            this.graveyardManager = graveyardManager;
            this.board = board;
            this.whiteKing = whiteKing;
            this.blackKing = blackKing;
        }

        public abstract void Enter();
        public abstract void Exit();
        public void MovePiece(Piece piece, BoardGrid grid)
        {
            var containerTransform = piece.Container.transform;
            var containerPosition = containerTransform.position;
            var gridPosition = grid.transform.position;
            containerPosition.x = gridPosition.x;
            containerPosition.z = gridPosition.z;
            containerTransform.position = containerPosition;
        }

        public virtual void SelectPiece(Piece piece)
        {
            chessManager.DeactiveAllActivedGrids();
            if (piece.Color != chessManager.Turn) return;
            SelectPieceState(piece);
        }

        protected abstract void SelectPieceState(Piece piece);

        public void SelectBoardGrid(BoardGrid grid)
        {
            var deadPiece = board.Move(selectedPiece, grid.Position);
            graveyardManager.AddPieceToGraveyard(deadPiece);
            MovePiece(selectedPiece, grid);
            LastPiece = selectedPiece;
            selectedPiece = null;
            chessManager.DeactiveAllActivedGrids();
            chessManager.SwitchTurn();
            UpdateNextState();
        }

        protected abstract void UpdateNextState();
    }
}