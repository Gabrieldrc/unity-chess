using System;

namespace Game.Core.Pieces
{
    public class King : Piece
    {
        public King(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "";
        protected override bool CanMove(Position position, ChessBoard board)
        {
            var stepInCol = Math.Abs(position.col - Position.col);
            var stepInRow = Math.Abs(position.row - Position.row);
            var steps = stepInCol > stepInRow ? stepInCol : stepInRow;
            if (steps != 1)
            {
                return false;
            }

            var destinyPiece = board.GetPieceIn(position);
            var isEmpty = destinyPiece is Empty;
            if (!isEmpty && destinyPiece.Color == Color)
            {
                return false;
            }

            return true;
        }
    }
}
