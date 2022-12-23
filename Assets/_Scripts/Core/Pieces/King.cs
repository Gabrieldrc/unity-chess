using System;
using System.Collections.Generic;

namespace Game.Core.Pieces
{
    public class King : Piece, ICloneable
    {
        public King(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "K";
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

            var boardIfKingMove = board.Clone() as ChessBoard;
            boardIfKingMove.Move(this.Clone() as King, position);
            var pieceColor = Color == PieceColor.Black ? PieceColor.White : PieceColor.Black;
            if (!(ChessBoard.FindPieceThatCanMoveTo(boardIfKingMove, position, pieceColor) is Empty))
            {
                return false;
            }

            return true;
        }

        public static List<Position> GetKingMove(King king, ChessBoard board)
        {
            var positions = new List<Position>();
            var kingPosition = king.Position;
            var row = Math.Clamp(kingPosition.row - 1, 0, board.Rows - 1);
            var maxRow = Math.Clamp(kingPosition.row + 1, 0, board.Rows - 1);
            while (row <= maxRow)
            {
                var col = Math.Clamp(kingPosition.col - 1, 0, board.Columns - 1);
                var maxCol = Math.Clamp(kingPosition.row + 1, 0, board.Rows - 1);
                while (col <= maxCol)
                {
                    var position = new Position(row, col);
                    if (king.CanMove(position, board))
                        positions.Add(new Position(position));
                    col++;
                }
                row++;
            }

            return positions;
        }
        
        public override List<Position> GetMiddlePositionsBetweenThisAndTarget(Position position, ChessBoard board)
            => new List<Position>();

        public object Clone()
        {
            return new King(Position, Color);
        }
    }
}
