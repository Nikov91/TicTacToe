using Microsoft.AspNetCore.Components;
using TicTacToe.Shared;
using TicTacToe.Shared.AnotherBitwise;
using TicTacToe.Shared.FunctionalBoard;
using TicTacToe.Shared.GridBitwise;
using TicTacToe.Shared.GridNormal;

namespace TicTacToe.Client.Pages
{
	public partial class Play
	{
		[Parameter] public int Mode { get; set; }
		//public Board Board { get; set; } = new Board();
		public MatrixBoard BoardMatrix { get; private set; } = null;
		public BitwiseBoard BoardBitwise { get; private set; } = new BitwiseBoard();
		//public AnotherBitwiseBoard AnotherBitwise { get; private set; } = new AnotherBitwiseBoard();
		public IBoard Board { get; private set; }

		protected override void OnParametersSet()
		{
			base.OnParametersSet();
			CreateBoard();
		}

		// (disclaimer) I don't know razor
		private void CreateBoard()
		{
			switch (Mode)
			{
				case 1: 
					Board = new Board();
					break;
				case 2:
					BoardMatrix = new MatrixBoard();
					BoardMatrix.Initialize();
					break;
				case 3:
					BoardBitwise = new BitwiseBoard();
					break;
				case 4:
					Board = new AnotherBitwiseBoard();
					break;
				default:
					Board = new Board();
					break;
			}
			
		}

		public void Refresh()
		{
			StateHasChanged();
		}

		private void Restart()
		{
			CreateBoard();
			Refresh();
		}
	}
}
