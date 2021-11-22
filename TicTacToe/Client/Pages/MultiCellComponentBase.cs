using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using TicTacToe.Shared;

namespace TicTacToe.Client.Pages
{
    public abstract class MultiCellComponentBase : ComponentBase
    {
        [Parameter] public IEnumerable<Cell> Cells { get; set; }
    }
}
