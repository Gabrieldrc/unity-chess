using System;

namespace Game
{
    [Serializable]
    public class Position
    {
        public int col;
        public int row;

        public Position(){}

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public Position(Position copy)
        {
            row = copy.row;
            col = copy.col;
        }

        public override string ToString()
        {
            return Utils.PositionToBoardPosition(row, col);
        }

        public bool EqualsTo(Position position)
        {
            return position.row == row && position.col == col;
        }
    }
}