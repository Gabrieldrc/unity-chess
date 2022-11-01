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
        protected Piece lastPiece;
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
            if (piece.Color != chessManager.Turn) return;
            selectedPiece = piece;
            var allMovePositions = piece.GetAllMovePositions(board);
            chessManager.DeactiveAllActivedGrids();
            chessManager.ActiveAllGridsInThisPostions(allMovePositions);
        }
        
        public void SelectBoardGrid(BoardGrid grid)
        {
            var deadPiece = board.Move(selectedPiece, grid.Position);
            graveyardManager.AddPieceToGraveyard(deadPiece);
            MovePiece(selectedPiece, grid);
            lastPiece = selectedPiece;
            selectedPiece = null;
            chessManager.DeactiveAllActivedGrids();
            chessManager.SwitchTurn();
            UpdateNextState();
        }

        protected abstract void UpdateNextState();
    }
}