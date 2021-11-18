namespace TicTacToe.Shared.FunctionalBoard
{
    public class Cell
    {
        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; }

        public int Column { get; }
    }
}
