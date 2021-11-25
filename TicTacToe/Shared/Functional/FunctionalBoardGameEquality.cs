using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TicTacToe.Shared.Functional
{
    internal class FunctionalBoardGameEquality : IEqualityComparer<FunctionalBoard>
    {
        public bool Equals(FunctionalBoard x, FunctionalBoard y) =>
            x.HomeMoves.SequenceEqual(y.HomeMoves) &&
            x.AwayMoves.SequenceEqual(y.AwayMoves);

        public int GetHashCode([DisallowNull] FunctionalBoard obj) =>
            obj.HomeMoves.Concat(obj.AwayMoves)
                .Select(cell => cell.GetHashCode())
                .DefaultIfEmpty(0)
                .Aggregate(HashCode.Combine);
    }
}
