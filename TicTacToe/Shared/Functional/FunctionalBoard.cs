using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TicTacToe.Shared.Functional
{
    public class FunctionalBoard : IImmutableBoard
    {
        public FunctionalBoard() : this(ImmutableList<Cell>.Empty)
        {
        }

        private FunctionalBoard(ImmutableList<Cell> moves)
        {
            this.Moves = moves;
        }

        private ImmutableList<Cell> Moves { get; }

        public IEnumerable<Cell> HomeMoves => this.GetMoves(0);
        public IEnumerable<Cell> AwayMoves => this.GetMoves(1);

        private IEnumerable<Cell> GetMoves(int parity) =>
            this.Moves
                .Select((move, offset) => (move, offset))
                .Where(tuple => tuple.offset % 2 == parity)
                .Select(tuple => tuple.move);

        public IEnumerable<Cell> PlayableCells =>
            this.WinningLines.Any() ? Enumerable.Empty<Cell>()
            : Cell.FullMatrix.Except(this.Moves);

            //this.WinningLines
            //    .Select(_ => Enumerable.Empty<Cell>())
            //    .DefaultIfEmpty(Cell.FullMatrix.Except(this.Moves))
            //    .First();

        public IEnumerable<Line> WinningLines =>
            this.WinningLinesFrom(this.HomeMoves).Concat(
                this.WinningLinesFrom(this.AwayMoves));

        private IEnumerable<Line> WinningLinesFrom(IEnumerable<Cell> moves) =>
            moves.SelectMany(cell => cell.Lines)
                .GroupBy(line => line)
                .Where(group => group.Count() == Constants.Dimension)
                .Select(group => group.Key);

        private IImmutableBoard Play(Cell cell) =>
            this.PlayConcrete(cell);

        private FunctionalBoard PlayConcrete(Cell cell) =>
            new FunctionalBoard(this.Moves.Add(cell));

        //public IEnumerable<IImmutableBoard> PlayableSelf() =>
        //    this.PlayableCells.Any() ? new[] { this }
        //    : Enumerable.Empty<IImmutableBoard>();

        //public IEnumerable<object> ViolatedRules(IMove move) =>
        //    Enumerable.Empty<object>();

        public IEnumerable<IMove> PossibleMoves =>
            this.PossibleConcreteMoves;

        private IEnumerable<FunctionalMove> PossibleConcreteMoves =>
            this.PlayableCells
                .Select(cell => new FunctionalMove(cell, this.PlayConcrete));

        private static IDictionary<FunctionalBoard, int> PositionToContinuationsCount { get; } =
            new Dictionary<FunctionalBoard, int>(PositionComparer);

        public int CountContinuations() =>
            this.FullContinuationsCount(0);

        private int CountContinuations(int weight) =>
            PositionToContinuationsCount.TryGetValue(this, out int count) ? count
            : this.FullContinuationsCount(weight);

        private int FullContinuationsCount(int weight)
        {
            int count = this.PossibleConcreteMoves
                .Select(move => move.MakeConcrete())
                .Select(next => next.CountContinuations(1))
                .DefaultIfEmpty(weight)
                .Sum();
            PositionToContinuationsCount[this] = count;
            if (PositionToContinuationsCount.Count % 1000 == 0)
                Console.WriteLine(PositionToContinuationsCount.Count);
            return count;
        }

        public static IEqualityComparer<FunctionalBoard> PositionComparer =>
            new FunctionalBoardPositionEquality();

        public static IEqualityComparer<FunctionalBoard> GameComparer =>
            new FunctionalBoardGameEquality();
    }
}
