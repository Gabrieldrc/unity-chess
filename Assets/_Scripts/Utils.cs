namespace Game
{
    public class Utils
    {
        private static string[] COLUMNS = new string[] {"a", "b", "c", "d", "e", "f", "g", "h"};
        private static string[] ROWS = new string[] { "1", "2", "3", "4", "5", "6", "7", "8"};

        public static string PositionToBoardPosition(int row, int col)
        {
            if (row < 0 || row > 7 || col < 0 || col > 7) return "";
            return COLUMNS[col] + ROWS[row];
        }
    }
}