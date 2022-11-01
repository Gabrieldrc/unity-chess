using System;
using System.Collections.Generic;
using Game.Core;
using Game.Core.GameStates;
using Game.Core.Pieces;
using Game.Managers;
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
        
        [SerializeField]
        private PieceGraveyardManager _graveyardManager;

        private ChessBoard _board;
        private Piece _selectedPiece;
        private List<BoardGrid> _activedGrids;
        private PieceColor _pieceColorTurn = PieceColor.White;
        private Winner _winner = Winner.NoOne;
        private Piece _whiteKing;
        private Piece _blackKing;
        private GameState _currentState;


        #region Unity
        
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

            NormalState = new NormalState(this, _graveyardManager, _board, _whiteKing, _blackKing);
            CheckState = new CheckState(this, _graveyardManager, _board, _whiteKing, _blackKing);
            _currentState = NormalState;
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

        #endregion

        public PieceColor Turn
        {
            get => _pieceColorTurn;
        }

        public GameState CheckState { get; private set; }
        public GameState NormalState { get; private set; }
        public GameState NextState { private get; set; } = null;

        public void SelectPiece(Piece piece)
        {
            _currentState.SelectPiece(piece);
        }

        public void SelectBoardGrid(BoardGrid grid)
        {
            _currentState.SelectBoardGrid(grid);
        }

        public void DeactiveAllActivedGrids()
        {
            foreach (var grid in _activedGrids)
            {
                grid.SetActive(false);
            }

            _activedGrids.Clear();
        }

        public void SwitchTurn()
        {
            _pieceColorTurn = _pieceColorTurn == PieceColor.Black ? PieceColor.White : PieceColor.Black;
        }

        public void ActiveAllGridsInThisPostions(List<Position> positions)
        {
            foreach (var position in positions)
            {
                var grid = _grids.Find(grid => grid.Position.Equals(position));
                if (grid == null)
                    continue;
                grid.SetActive(true);
                _activedGrids.Add(grid);
            }
        }

        private void BuildBoard()
        {
            _board = new ChessBoard();
            foreach (var pieceContainer in _pieces)
            {
                pieceContainer.OnSelectEvent += SelectPiece;
                var piece = pieceContainer.Piece;
                _board.AddPiece(piece);
                if (piece is King)
                {
                    if (piece.Color == PieceColor.White)
                    {
                        _whiteKing = piece;
                    }
                    else if (piece.Color == PieceColor.Black)
                    {
                        _blackKing = piece;
                    }
                }
            }
        }

        private void ChangeState()
        {
            if (NextState == null) return;
            _currentState = NextState;
            NextState = null;
        }
    }
}