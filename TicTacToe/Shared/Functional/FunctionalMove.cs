using System;

namespace TicTacToe.Shared.Functional
{
    class FunctionalMove : IMove
    {
        public FunctionalMove(Cell location, Func<Cell, FunctionalBoard> map)
        {
            this.Location = location;
            this.Map = map;
        }

        public Cell Location { get; }
        private Func<Cell, FunctionalBoard> Map { get; }

        public IImmutableBoard Make() => this.MakeConcrete();

        public FunctionalBoard MakeConcrete() => this.Map(this.Location);
    }
}
