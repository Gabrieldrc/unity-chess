using System;
using System.Collections.Generic;
using Game._Scripts.Core.Notations;
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
        
        [SerializeField]
        private GameState _initialState;

        private ChessBoard _board;
        private Piece _selectedPiece;
        private List<BoardGrid> _activedGrids;
        private PieceColor _pieceColorTurn = PieceColor.White;
        private Winner _winner = Winner.NoOne;
        private Piece _whiteKing;
        private Piece _blackKing;
        private GameState _currentState;
        [SerializeField] private HistoryManager _historyManager;
        public PieceColor Turn { get => _pieceColorTurn; }
        public ChessBoard Board { get => _board; }
        public Piece WhiteKing
        {
            get => _whiteKing;
            private set => _whiteKing = value;
        }

        public Piece BlackKing
        {
            get => _blackKing;
            private set => _blackKing = value;
        }



        #region Unity
        
        private void Start()
        {
            _activedGrids = new List<BoardGrid>();
            // if (_pieces.Count != 32)
            // {
            //     Debug.LogError("It has to have 32 pieces");
            // }

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
            _currentState = _initialState;
            var checkpoint = new Checkpoint(
                Board,
                Turn,
                null,
                null,
                null,
                _currentState
            );
            _historyManager.AddCheckpoint(checkpoint);
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
            OnTurnChangeEvent?.Invoke(_pieceColorTurn);
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

        public void ChangeState(GameState nextState)
        {
            _currentState.Exit();
            nextState.LastPiece = _currentState.LastPiece;
            _currentState = nextState;
            _currentState.Enter();
        }

        public void SetGameState(Checkpoint lastCheckpoint)
        {
            _currentState.Exit();
            _board = lastCheckpoint.Board;
            _pieceColorTurn = lastCheckpoint.Turn;
            _currentState = lastCheckpoint.CurrentState;
            _currentState.LastPiece = lastCheckpoint.LastPiece;
            _currentState.Enter();
        }
    }
}