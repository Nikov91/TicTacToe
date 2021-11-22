using Microsoft.AspNetCore.Components;
using TicTacToe.Shared;

namespace TicTacToe.Client.Pages
{
    public abstract class CellComponentBase : ComponentBase
    {
        [Parameter] public Cell Location { get; set; }

        protected int X => 325 * Location.Column;
        protected int Y => 325 * Location.Row;
    }
}
