using Microsoft.AspNetCore.Components;
using TicTacToe.Shared.GridBitwise;
using TicTacToe.Shared.GridNormal;

namespace TicTacToe.Client.Pages
{
    public partial class Play
    {
        [Parameter] public int Mode { get; set; }
        public GameBoard GameBoard { get; private set; } = null;
        public GameBoardBW GameBoardBW { get; private set; } = null;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            CreateBoards();
        }

        // (disclaimer) I don't know razor
        private void CreateBoards()
        {
            GameBoard = new GameBoard();
            GameBoard.Initialize();

            GameBoardBW = new GameBoardBW();
            GameBoardBW.Initialize();
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        private void Restart()
        {
            if (Mode == 2)
                GameBoard.Initialize();
            else if (Mode == 3)
                GameBoardBW.Initialize();

            Refresh();
        }
    }
}
