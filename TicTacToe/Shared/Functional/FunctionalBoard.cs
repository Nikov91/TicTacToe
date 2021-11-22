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
          Cell.FullMatrix.Except(this.Moves);

        public IEnumerable<Line> WinningLines => Enumerable.Empty<Line>();

        public IImmutableBoard Play(Cell cell) =>
            new FunctionalBoard(this.Moves.Add(cell));

        public int CountContinuations() => 0;
    }
}
