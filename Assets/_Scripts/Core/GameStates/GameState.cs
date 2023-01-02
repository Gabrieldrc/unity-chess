using Game._Scripts.Core.Notations;
using Game.Components;
using Game.Managers;
using UnityEngine;

namespace Game.Core.GameStates
{
    public abstract class GameState : MonoBehaviour
    {
        [SerializeField]
        protected ChessManager chessManager;

        [SerializeField]
        protected PieceGraveyardManager graveyardManager;

        [SerializeField]
        protected HistoryManager historyManager;

        protected Piece selectedPiece;
        private ChessBoard _board;
        private Piece _whiteKing;
        private Piece _blackKing;
        protected GameState _nextState;

        public Piece WhiteKing
        {
            get
            {
                if (_whiteKing == null)
                {
                    _whiteKing = chessManager.WhiteKing;
                }

                return _whiteKing;
            }
        }
        public Piece BlackKing
        {
            get
            {
                if (_blackKing == null)
                {
                    _blackKing = chessManager.BlackKing;
                }

                return _blackKing;
            }
        }
        public Piece LastPiece { get; set; } = null;

        public ChessBoard Board
        {
            get
            {
                if (_board == null)
                {
                    _board = chessManager.Board;
                }

                return _board;
            }
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
            var deadPiece = _board.Move(selectedPiece, grid.Position);
            graveyardManager.AddPieceToGraveyard(deadPiece);
            MovePiece(selectedPiece, grid);
            chessManager.DeactiveAllActivedGrids();
            chessManager.SwitchTurn();
            UpdateNextState();
            AddCheckpoint(grid, deadPiece);
            LastPiece = selectedPiece;
            selectedPiece = null;
            ChangeToNextState();
        }

        private void ChangeToNextState()
        {
            if (_nextState == null || _nextState == this) return;
            var next = _nextState;
            _nextState = null;
            chessManager.ChangeState(next);
        }

        private void AddCheckpoint(BoardGrid grid, Piece deadPiece)
        {
            var newNotation = new Checkpoint(
                Board,
                chessManager.Turn,
                LastPiece,
                new PieceData(
                    selectedPiece,
                    selectedPiece.Container.transform.position,
                    grid.transform.position,
                    selectedPiece.Position,
                    grid.Position
                ),
                deadPiece,
                _nextState
            );
            historyManager.AddCheckpoint(newNotation);
        }

        protected abstract void UpdateNextState();
    }
}