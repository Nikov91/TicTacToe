using System;

namespace TicTacToe.Shared.Functional
{
    class FunctionalMove : IMove
    {
        public FunctionalMove(Cell location, Func<Cell, IImmutableBoard> map)
        {
            this.Location = location;
            this.Map = map;
        }

        public Cell Location { get; }
        private Func<Cell, IImmutableBoard> Map { get; }

        public IImmutableBoard Make() => this.Map(this.Location);
    }
}
