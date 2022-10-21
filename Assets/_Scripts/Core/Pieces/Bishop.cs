using System;
using UnityEngine;

namespace Game.Core.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "";
        protected override bool CanMove(Position position, ChessBoard board)
        {
            Piece destinationPiece;
            if (!CanMoveDiagonally(Position, position, board))
            {
                return false;
            }

            destinationPiece = board.GetPieceIn(position);

            if (!(destinationPiece is Empty) && destinationPiece.Color == Color)
            {
                return false;
            }

            return true;
        }

        public static bool CanMoveDiagonally(Position from, Position to, ChessBoard board)
        {
            var itMoveInCol = from.col != to.col;
            var itMoveInRow = from.row != to.row;

            if (to.EqualsTo(from))
            {
                return false;
            }

            if ((!itMoveInCol && itMoveInRow) || (itMoveInCol && !itMoveInRow))
            {
                return false;
            }

            var stepsInRow = Math.Abs(to.row - from.row);
            var stepsInCol = Math.Abs(to.col - from.col);

            if (stepsInCol != stepsInRow)
            {
                return false;
            }

            var rowDirection = to.row < from.row ? -1 : 1;
            var colDirection = to.col < from.col ? -1 : 1;

            Piece destinationPiece;
            var indexRow = from.row + rowDirection;
            var indexCol = from.col + colDirection;
            while (indexRow != to.row && indexRow < board.Rows && indexRow >= 0 && indexCol != to.col && indexCol < board.Columns && indexCol >= 0)
            {
                destinationPiece = board.GetPieceIn(new Position(indexRow, indexCol));
                if (!(destinationPiece is Empty))
                    return false;

                indexCol += colDirection;
                indexRow += rowDirection;
            }

            return true;
        }
    }
}
