using System;
using System.Collections.Generic;

namespace Game.Core.Pieces
{
    public class King : Piece
    {
        public King(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "";
        public override bool CanMove(Position position, ChessBoard board)
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

            var pieceColor = Color == PieceColor.Black ? PieceColor.White : PieceColor.Black;
            if (!(board.FindPieceThatCanMoveTo(position, pieceColor) is Empty))
            {
                return false;
            }

            return true;
        }

        public static List<Position> GetKingMove(King king, ChessBoard board)
        {
            var positions = new List<Position>();
            var kingPosition = king.Position;
            for (int row = kingPosition.row; row == kingPosition.row + 1; row++)
            {
                for (int col = kingPosition.col - 1; col == kingPosition.col + 1; col++)
                {
                    var position = new Position(row, col);
                    if (king.CanMove(position, board))
                        positions.Add(new Position(position));
                }
            }

            return positions;
        }
        
        public override List<Position> GetMiddlePositionsBetweenThisAndTarget(Position position, ChessBoard board)
            => new List<Position>();
    }
}
