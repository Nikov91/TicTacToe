using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Shared
{
    public record Cell
    {
        public Cell(int row, int column)
        {
            this.Row = row >= 0 && row < Constants.Dimension ? row : throw new ArgumentException(nameof(row));
            this.Column = column >= 0 && column < Constants.Dimension ? column : throw new ArgumentException(nameof(column));
        }

        public int Row { get; }
        public int Column { get; }

        public static Cell UpperLeft => new Cell(Constants.Up, Constants.Left);
        public static Cell UpperRight => new Cell(Constants.Up, Constants.Right);
        public static Cell LowerLeft => new Cell(Constants.Down, Constants.Left);
        public static Cell LowerRight => new Cell(Constants.Down, Constants.Right);

        public IEnumerable<Line> Lines
        {
            get
            {
                yield return Line.Row(this.Row);
                yield return Line.Column(this.Column);
                if (this.Row == this.Column) yield return Line.Diagonal();
                if (this.Row + this.Column + 1 == Constants.Dimension) yield return Line.Antidiagonal();
            }
        }

        public static IEnumerable<Cell> FullMatrix =>
          Enumerable.Range(0, Constants.Dimension).SelectMany(FullRow);

        private static IEnumerable<Cell> FullRow(int row) =>
            Enumerable.Range(0, Constants.Dimension).Select(col => new Cell(row, col));
    }
}
