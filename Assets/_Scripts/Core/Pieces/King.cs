using System;
using System.Collections.Generic;

namespace Game.Core.Pieces
{
    public class King : Piece, ICloneable
    {
        public King(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "KNG";
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
            for (int row = kingPosition.row - 1; row < kingPosition.row + 2; row++)
            {
                for (int col = kingPosition.col - 1; col < kingPosition.col + 2; col++)
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

        public object Clone()
        {
            return new King(Position, Color);
        }
    }
}
