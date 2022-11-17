using System;
using System.Collections.Generic;
using Game.Core.Pieces;
using UnityEngine;

namespace Game.Core
{
    public class ChessBoard
    {
        private Piece[,] _board;

        public ChessBoard()
        {
            _board = new Piece[8, 8];
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    _board[row, col] =
                        PieceFactory.GetEmptyPiece(
                            LocalPositionToGamePosition(new Position(row, col))
                        );
                }
            }
        }

        public int Rows
        {
            get => _board.GetLength(0);
        }
        
        public int Columns
        {
            get => _board.GetLength(1);
        }

        public void AddPiece(Piece piece)
        {
            var piecePosition = piece.Position;
            var localPosition = GamePositionToLocalPosition(piecePosition);
            _board[localPosition.row, localPosition.col] = piece;
        }

        public Piece Move(Piece piece, Position to)
        {
            var pieceLocalPosition = GamePositionToLocalPosition(piece.Position);
            var toLocalPosition = GamePositionToLocalPosition(to);
            var destinationPiece = _board[toLocalPosition.row, toLocalPosition.col];
            _board[pieceLocalPosition.row, pieceLocalPosition.col] =
                PieceFactory.GetEmptyPiece(piece.Position);
            piece.Move(to, destinationPiece);
            _board[toLocalPosition.row, toLocalPosition.col] = piece;

            return destinationPiece;
        }

        public Piece GetPieceIn(Position position)
        {
            var localPosition = GamePositionToLocalPosition(position);
            return _board[localPosition.row, localPosition.col];
        }

        public Piece FindPieceThatCanMoveTo(Position position, PieceColor pieceColor)
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    var piece = GetPieceIn(new Position(row, col));
                    if (piece.Color != pieceColor)
                        continue;
                    if (piece.CanMove(position, this))
                        return piece;
                }
            }
            return PieceFactory.GetEmptyPiece(position);
        }

        public List<Piece> GetAllPiecesByColor(PieceColor color)
        {
            var pieces = new List<Piece>();
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    var piece = GetPieceIn(new Position(row, col));
                    if (piece.Color != color)
                        continue;
                    pieces.Add(piece);
                }
            }

            return pieces;
        }

        private Position GamePositionToLocalPosition(Position position)
        {
            return ChangeTypeOfPosition(position);
        }

        private Position LocalPositionToGamePosition(Position position)
        {
            return ChangeTypeOfPosition(position);
        }
        
        private Position ChangeTypeOfPosition(Position position)
        {
            var newPosition = new Position();
            newPosition.col = position.col;
            newPosition.row = Math.Abs(position.row - 7);

            return newPosition;
        }
    }
}