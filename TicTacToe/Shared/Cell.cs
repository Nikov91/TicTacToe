using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Shared
{
    public record Cell(int Row, int Column)
    {
        public static IEnumerable<Cell> FullMatrix =>
          Enumerable.Range(0, Constants.Dimensions).SelectMany(FullRow);

        private static IEnumerable<Cell> FullRow(int row) =>
            Enumerable.Range(0, Constants.Dimensions).Select(col => new Cell(row, col));
    }
}
