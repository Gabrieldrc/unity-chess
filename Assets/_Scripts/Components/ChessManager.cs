using System;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Components
{
    public class ChessManager : MonoBehaviour
    {
        public event Action<PieceColor> OnTurnChangeEvent;
        public event Action<Winner> OnGameOverEvent;
        public event Action<Piece> OnMovementEvent; 

        [SerializeField, Tooltip("It has to have 32 pieces")]
        private List<PieceContainer> _pieces;

        [SerializeField, Tooltip("It has to have 64 grids")]
        private List<BoardGrid> _grids;

        private ChessBoard _board;
        private Piece _selectedPiece;
        private List<BoardGrid> _activedGrids;
        private PieceColor _pieceColorTurn = PieceColor.White;
        private Winner _winner = Winner.NoOne;


        private void Start()
        {
            _activedGrids = new List<BoardGrid>();
            if (_pieces.Count != 32)
            {
                Debug.LogError("It has to have 32 pieces");
            }

            if (_grids.Count != 64)
            {
                Debug.LogError("It has to have 64 grids");
            }
            BuildBoard();

            foreach (var grid in _grids)
            {
                grid.OnSelectEvent += SelectBoardGrid;
                grid.SetActive(false);
            }
        }

        private void OnDisable()
        {
            foreach (var pieceContainer in _pieces)
            {
                pieceContainer.OnSelectEvent -= SelectPiece;
            }
            foreach (var grid in _grids)
            {
                grid.OnSelectEvent -= SelectBoardGrid;
            }
        }

        public PieceColor Turn
        {
            get => _pieceColorTurn;
        }
        
        public Winner Winner
        {
            get => _winner;
        }

        public void SelectPiece(Piece piece)
        {
            if (piece.Color != Turn) return;
            _selectedPiece = piece;
            var allMovePositions = piece.GetAllMovePositions(_board);
            DeactiveAllActivedGrids();
            foreach (var position in allMovePositions)
            {
                var grid = _grids.Find(grid => grid.Position.EqualsTo(position));
                if (grid == null)
                    continue;
                grid.SetActive(true);
                _activedGrids.Add(grid);
            }
        }

        public void SelectBoardGrid(BoardGrid grid)
        {
            _board.Move(_selectedPiece, grid.Position);
            MovePiece(_selectedPiece, grid);
            _selectedPiece = null;
            DeactiveAllActivedGrids();
            SwitchTurn();
        }

        public void GameOver()
        {
            
        }

        public bool IsDraw()
        {
            return false;
        }

        private void MovePiece(Piece piece, BoardGrid grid)
        {
            var containerTransform = piece.Container.transform;
            var containerPosition = containerTransform.position;
            var gridPosition = grid.transform.position;
            containerPosition.x = gridPosition.x;
            containerPosition.z = gridPosition.z;
            containerTransform.position = containerPosition;
        }
        
        private void BuildBoard()
        {
            _board = new ChessBoard();
            foreach (var pieceContainer in _pieces)
            {
                pieceContainer.OnSelectEvent += SelectPiece;
                var piece = pieceContainer.Piece;
                _board.AddPiece(piece);
            }
        }

        private void DeactiveAllActivedGrids()
        {
            foreach (var grid in _activedGrids)
            {
                grid.SetActive(false);
            }

            _activedGrids.Clear();
        }

        private void SwitchTurn()
        {
            _pieceColorTurn = _pieceColorTurn == PieceColor.Black ? PieceColor.White : PieceColor.Black;
        }
    }
}