using System.Collections.Generic;

namespace TicTacToe.Shared
{
    public interface IImmutableBoard : IBoard
    {
        IEnumerable<IMove> PossibleMoves { get; }
    }
}
