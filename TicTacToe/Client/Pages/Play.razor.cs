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
            CreateBoardAndInit(Mode);
        }

        // (disclaimer) I don't know razor
        private void CreateBoardAndInit(int boardType)
        {
            switch (boardType)
            {
                case 2: // normal
                    GameBoard = new GameBoard();
                    GameBoard.Initialize();
                    break;
                case 3: // bitwise
                    GameBoardBW = new GameBoardBW();
                    GameBoardBW.Initialize();
                    break;
                default:
                    break;
            }
        }
        public void Refresh()
        {
            StateHasChanged();
        }

        private void Restart()
        {
            if (Mode == 2)
                GameBoard.Initialize();
            if (Mode == 3)
                GameBoardBW.Initialize();

            Refresh();
        }
    }
}
