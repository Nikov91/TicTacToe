using System;

namespace TicTacToe.Shared.Functional
{
    public class FunctionalMove : IMove
    {
        public FunctionalMove(Cell location, Func<Cell, FunctionalBoard> map)
        {
            this.Location = location;
            this.Map = map;
        }

        public Cell Location { get; }
        private Func<Cell, FunctionalBoard> Map { get; }

        IImmutableBoard IMove.Make() => this.Make();
        public FunctionalBoard Make() => this.Map(this.Location);
    }
}
