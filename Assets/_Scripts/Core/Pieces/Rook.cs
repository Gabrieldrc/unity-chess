using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Pieces
{
    public class Rook : Piece
    {
        public Rook(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "R";
        public override bool CanMove(Position position, ChessBoard board)
        {
            Piece destinationPiece;
            if (!CanMoveHorizontalOrVertical(Position, position, board))
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

        public static bool CanMoveHorizontalOrVertical(Position from, Position to, ChessBoard board)
        {
            var itMoveInCol = from.col != to.col;
            var itMoveInRow = from.row != to.row;

            if (itMoveInCol && itMoveInRow)
            {
                return false;
            }

            Piece destinationPiece;
            if (itMoveInCol)
            {
                var colMultiplier = to.col < from.col ? -1 : 1;
                var colIndex = from.col + (1 * colMultiplier);
                while (colIndex != to.col)
                {
                    destinationPiece = board.GetPieceIn(new Position(from.row, colIndex));
                    if (!(destinationPiece is Empty))
                        return false;

                    colIndex += 1 * colMultiplier;
                }
            }
            else
            {
                var rowMultiplier = to.row < from.row ? -1 : 1;
                var rowIndex = from.row + (1 * rowMultiplier);
                while (rowIndex != to.row)
                {
                    destinationPiece = board.GetPieceIn(new Position(rowIndex, from.col));
                    if (!(destinationPiece is Empty))
                        return false;

                    rowIndex += 1 * rowMultiplier;
                }
            }

            return true;
        }

        public override List<Position> GetMiddlePositionsBetweenThisAndTarget(Position position, ChessBoard board)
        {
            return GetMiddlePositionsBetweenThisAndTargetHV(this, position, board);
        }
        
        public static List<Position> GetMiddlePositionsBetweenThisAndTargetHV(Piece piece, Position position, ChessBoard board)
        {
            var positions = new List<Position>();
            if (piece.CanMove(position, board))
            {
                var movingH = position.row == piece.Position.row;
                var multiplierRow = 0;
                var multiplierCol = 0;
                
                if (movingH)
                {
                    multiplierCol = position.col > piece.Position.col ? 1 : -1;
                }
                else
                {
                    multiplierRow = position.row > piece.Position.row ? 1 : -1;
                }
                
                var indexPosition = piece.Position;
                indexPosition.row += multiplierRow;
                indexPosition.col += multiplierCol;
                while (!position.Equals(indexPosition))
                {
                    if (piece.CanMove(indexPosition, board))
                        positions.Add(indexPosition);
                    indexPosition.row += multiplierRow;
                    indexPosition.col += multiplierCol;
                }
            }
            return positions;
        }
    }
}

