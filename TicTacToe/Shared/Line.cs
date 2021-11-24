namespace TicTacToe.Shared
{
    public record Line
    {
        public Line(Cell from, Cell to)
        {
            this.From = from;
            this.To = to;
        }

        public Cell From { get; }
        public Cell To { get; }

        public static Line Row(int offset) =>
            new Line(new Cell(offset, Constants.Up), new Cell(offset, Constants.Down));

        public static Line Column(int offset) =>
            new Line(new Cell(Constants.Left, offset), new Cell(Constants.Right, offset));

        public static Line Diagonal() =>
            new Line(Cell.UpperLeft, Cell.LowerRight);

        public static Line Antidiagonal() =>
            new Line(Cell.UpperRight, Cell.LowerLeft);
    }
}
