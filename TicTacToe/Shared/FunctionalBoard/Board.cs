using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Shared.FunctionalBoard
{
	public class Board : IBoard
	{
		public IEnumerable<Cell> HomeMoves => Enumerable.Empty<Cell>();
		public IEnumerable<Cell> AwayMoves => Enumerable.Empty<Cell>();
		
		public IEnumerable<Cell> PlayableCells =>
			Enumerable.Range(0, 9).Select(i => new Cell(i / 3, i % 3));

		public IEnumerable<Line> WinningLines => Enumerable.Empty<Line>();
		public void Play(Cell cell)
		{
			throw new System.NotImplementedException();
		}
	}
}
