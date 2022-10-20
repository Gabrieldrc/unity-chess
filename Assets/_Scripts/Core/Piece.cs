using System.Collections.Generic;
using Game.Components;
using Game.Core.Pieces;

namespace Game.Core
{
    public abstract class Piece
    {
        private PieceColor _color;
        private Position _position;
        private string _lastMovement = "";
        private PieceContainer _container;

        public Piece(Position position, PieceColor color)
        {
            this._position = position;
            _color = color;
        }

        public PieceContainer Container
        {
            get => _container;
            set
            {
                if (_container == null)
                {
                    _container = value;
                }
            }
        }
        public PieceColor Color
        {
            get => _color;
            protected set => _color = value;
        }

        public Position Position
        {
            get => new Position(this._position);
            set
            {
                _position = value;
            }
        }

        public string LastMovement
        {
            get => _lastMovement;
            private set => _lastMovement = value;
        }
        
        public abstract string Sign { get; set; }

        public virtual void Move(Position to, Piece destinationPiece)
        {
            string eat = destinationPiece is Empty ? "" : "x";
            LastMovement = $"{Sign}{eat}{_position.ToString()}";
            Position = to;
        }


        public virtual List<Position> GetAllMovePositions(ChessBoard board)
        {
            var movePositions = new List<Position>();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (CanMove(new Position(row, col), board))
                        movePositions.Add(new Position(row, col));
                }
            }

            return movePositions;
        }

        protected abstract bool CanMove(Position position, ChessBoard board);
    }
}
