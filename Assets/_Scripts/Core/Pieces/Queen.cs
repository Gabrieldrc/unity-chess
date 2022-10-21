namespace Game.Core.Pieces
{
    public class Queen : Piece
    {
        public Queen(Position position, PieceColor color) : base(position, color)
        {
        }
        public override string Sign { get; set; } = "";
        public override bool CanMove(Position position, ChessBoard board)
        {
            if (Rook.CanMoveHorizontalOrVertical(Position, position, board))
            {
                return IsPieceDestinyEmptyOrAnEnemy(position, board);
            }

            if (Bishop.CanMoveDiagonally(Position, position, board))
            {
                return IsPieceDestinyEmptyOrAnEnemy(position, board);
            }

            return false;
        }

        private bool IsPieceDestinyEmptyOrAnEnemy(Position position, ChessBoard board)
        {
            var piece = board.GetPieceIn(position);
            if (piece is Empty)
            {
                return true;
            }

            if (piece.Color != Color)
            {
                return true;
            }

            return false;
        }
    }
}

