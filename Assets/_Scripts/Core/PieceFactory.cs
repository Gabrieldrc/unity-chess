using Game.Core;
using Game.Core.Pieces;

namespace Game
{
    public static class PieceFactory
    {
        public static Piece GetPiece(Position position, PieceType pieceType, PieceColor pieceColor)
        {
            Piece piece;
            switch (pieceType)
            {
                case PieceType.Pawn:
                    piece = new Pawn(position, pieceColor);
                    break;
                case PieceType.Rook:
                    piece = new Rook(position, pieceColor);
                    break;
                case PieceType.Bishop:
                    piece = new Bishop(position, pieceColor);
                    break;
                case PieceType.King:
                    piece = new King(position, pieceColor);
                    break;
                case PieceType.Queen:
                    piece = new Queen(position, pieceColor);
                    break;
                case PieceType.Knight:
                    piece = new Knight(position, pieceColor);
                    break;
                default:
                    piece = new Empty(position);
                    break;
            }

            return piece;
        }

        public static Piece GetEmptyPiece(Position position)
        {
            return new Empty(position);
        }
    }
    
}