using System.Collections.Generic;

namespace TicTacToe.Shared
{
    public interface IBoard
    {
        IEnumerable<Cell> HomeMoves { get; }
        IEnumerable<Cell> AwayMoves { get; }
        IEnumerable<Cell> PlayableCells { get; }
        IEnumerable<Line> WinningLines { get; }
    }
}
