using Microsoft.AspNetCore.Components;
using TicTacToe.Shared;
using TicTacToe.Shared.AnotherBitwise;
using TicTacToe.Shared.Functional;
using TicTacToe.Shared.GridBitwise;
using TicTacToe.Shared.GridNormal;

namespace TicTacToe.Client.Pages
{
    public partial class Play
    {
        [Parameter] public int Mode { get; set; }
        public IBoard Board { get; private set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            CreateBoard();
        }

        private void CreateBoard()
        {
            this.Board = this.Mode switch
            {
                1 => new FunctionalBoard(),
                2 => new MatrixBoard(),
                3 => new BitwiseBoard(),
                4 => new AnotherBitwiseBoard(),
                _ => throw new System.Exception("Surprise, surprise!")
            };
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        private void MakeMove(Cell location)
        {
            if (this.Board is IImmutableBoard immutable)
            {
                this.Board = immutable.Play(location);
            }
            else if (this.Board is IMutableBoard mutable)
            {
                mutable.Play(location);
            }

            this.Refresh();
        }

        private void Restart()
        {
            CreateBoard();
            Refresh();
        }
    }
}
