namespace Game.Core.Pieces
{
    public class Rook : Piece
    {
        public Rook(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "";
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
    }
}

