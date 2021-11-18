using Microsoft.AspNetCore.Components;
using TicTacToe.Shared.FunctionalBoard;
using TicTacToe.Shared.GridBitwise;
using TicTacToe.Shared.GridNormal;

namespace TicTacToe.Client.Pages
{
    public partial class Play
    {
        [Parameter] public int Mode { get; set; }
        public Board Board { get; set; } = new Board();
        public MatrixBoard BoardMatrix { get; private set; } = null;
        public BitwiseBoard BoardBitwise { get; private set; } = new BitwiseBoard();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            CreateBoard();
        }

        // (disclaimer) I don't know razor
        private void CreateBoard()
        {
            BoardMatrix = new MatrixBoard();
            BoardMatrix.Initialize();
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        private void Restart()
        {
            Board = new Board();
            BoardMatrix.Initialize();
            BoardBitwise = new BitwiseBoard();

            Refresh();
        }
    }
}
