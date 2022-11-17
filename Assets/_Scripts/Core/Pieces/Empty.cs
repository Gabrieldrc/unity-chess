using System.Collections.Generic;

namespace Game.Core.Pieces
{
    public class Empty : Piece
    {
        public Empty(Position position) : base(position, PieceColor.Empty)
        {
        }

        public override string Sign { get; set; } = "";

        public override List<Position> GetAllMovePositions(ChessBoard board)
            => new List<Position>();

        public override bool CanMove(Position position, ChessBoard board)
            => false;

        public override List<Position> GetMiddlePositionsBetweenThisAndTarget(Position position, ChessBoard board)
            => new List<Position>();
    }
}