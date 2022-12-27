namespace Game._Scripts.Core.Notations
{
    public class NotationData
    {
        public int moved = -1;
        public string whiteMove = "";
        public string blackMove = "";
        
        public NotationData() {}

        public NotationData(int moved, string whiteMove, string blackMove)
        {
            this.moved = moved;
            this.whiteMove = whiteMove;
            this.blackMove = blackMove;
        }
    }
}