using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TicTacToe.Shared.Functional
{
    internal class FunctionalBoardPositionEquality : IEqualityComparer<FunctionalBoard>
    {
        public bool Equals(FunctionalBoard x, FunctionalBoard y) =>
            this.OrderedHome(x).SequenceEqual(this.OrderedHome(y)) &&
            this.OrderedAway(x).SequenceEqual(this.OrderedAway(y));

        public int GetHashCode([DisallowNull] FunctionalBoard obj) =>
            this.OrderedHome(obj).Concat(this.OrderedAway(obj))
                .Select(cell => cell.GetHashCode())
                .DefaultIfEmpty(0)
                .Aggregate(HashCode.Combine);

        private IEnumerable<Cell> OrderedHome(FunctionalBoard board) =>
            board.HomeMoves.OrderBy(x => x);

        private IEnumerable<Cell> OrderedAway(FunctionalBoard board) =>
            board.AwayMoves.OrderBy(x => x);
    }
}
