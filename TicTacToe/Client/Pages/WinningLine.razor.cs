using Microsoft.AspNetCore.Components;
using System;
using TicTacToe.Shared;

namespace TicTacToe.Client.Pages
{
    public partial class WinningLine
    {
        [Parameter] public Line Line { get; set; }
        public Cell From => this.Line.From;
        public Cell To => this.Line.To;

        public int FromX(int padding) => From.Column * 325 + 150 - (105 + padding) * XDirection;
        public int FromY(int padding) => From.Row * 325 + 150 - (105 + padding) * YDirection;
        public int ToX(int padding) => To.Column * 325 + 150 + (105 + padding) * XDirection;
        public int ToY(int padding) => To.Row * 325 + 150 + (105 + padding) * YDirection;

        private int XDirection =>
            Math.Sign(To.Column - From.Column);

        private int YDirection =>
            Math.Sign(To.Row - From.Row);
    }
}
