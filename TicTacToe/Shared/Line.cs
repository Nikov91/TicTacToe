namespace TicTacToe.Shared
{
    public class Line
    {
        public Line(Cell from, Cell to)
        {
            this.From = from;
            this.To = to;
        }

        public Cell From { get; }

        public Cell To { get; }
    }
}
