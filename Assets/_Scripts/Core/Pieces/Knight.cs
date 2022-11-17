using System;
using System.Collections.Generic;

namespace Game.Core.Pieces
{
    public class Knight : Piece
    {
        public Knight(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "";
        public override bool CanMove(Position position, ChessBoard board)
        {
            var stepsInRow = Math.Abs(position.row - Position.row);
            var stepsInCol = Math.Abs(position.col - Position.col);
            var allowMovement = (stepsInCol == 1 && stepsInRow == 2)
                                || (stepsInRow == 1 && stepsInCol == 2);
            if (!allowMovement)
            {
                return false;
            }

            var piece = board.GetPieceIn(position);
            var isAFriend = !(piece is Empty) && piece.Color == Color;
            if (isAFriend)
            {
                return false;
            }

            return true;
        }

        public override List<Position> GetMiddlePositionsBetweenThisAndTarget(Position position, ChessBoard board)
            => new List<Position>();
    }
}
