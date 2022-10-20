using System;

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

            var rowMultiplier = to.row < from.row ? -1 : 1;
            var colMultiplier = to.col < from.col ? -1 : 1;

            Piece destinationPiece;
            var indexRow = from.row + (1 * rowMultiplier);
            while (indexRow != to.row)
            {
                var indexCol = from.col + (1 * colMultiplier);
                while (indexCol != to.col)
                {
                    var piece = board.GetPieceIn(new Position(indexRow, indexCol));
                    if (!(piece is Empty))
                        return false;
                    indexCol += 1 * colMultiplier;
                }

                indexRow += 1 * rowMultiplier;
            }

            return true;
        }
    }
}
