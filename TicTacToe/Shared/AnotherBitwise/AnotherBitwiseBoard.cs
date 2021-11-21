using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Shared.AnotherBitwise
{
	public class AnotherBitwiseBoard : IBoard
	{
		private readonly uint[] _winningPositions = new uint[]
		{
			0b_111_000_000,
			0b_000_111_000,
			0b_000_000_111,
			0b_100_100_100,
			0b_010_010_010,
			0b_001_001_001,
			0b_100_010_001,
			0b_001_010_100
		};

		private int playerX = 0b_000_000_001;
		private int _activePlayer = 0b_000_000_001;

		private int _board;
		private int _occupied;
		private bool _isGameOver;

		public void Restart()
		{
			_board = 0b_000_000_000;
			_occupied = 0b_000_000_000;
			_activePlayer = playerX;
			_isGameOver = false;
		}

		public IEnumerable<Cell> HomeMoves => GetHomeMoves();
		public IEnumerable<Cell> AwayMoves => GetAwayMoves();
		public IEnumerable<Cell> PlayableCells => GetPlayableCells();
		public IEnumerable<Line> WinningLines => GetWinningLines();

		public void Play(Cell cell)
		{
			int position = Base3StringToInt($"{cell.Row}{cell.Column}");
			_board = _board | _activePlayer << position;
			_occupied = _occupied | 0b_000_000_001 << position;
			GetWinner();
			_activePlayer = playerX ^ _activePlayer;
		}

		private IEnumerable<Cell> GetPlayableCells()
		{
			return Enumerable
				.Range(0, 9)
				.Where(c => (_occupied & (1 << c)) == 0 && !_isGameOver)
				.Select(c => new Cell(c / 3, c % 3));
		}

		private IEnumerable<Cell> GetHomeMoves()
		{
			return Enumerable
				.Range(0, 9)
				.Where(c => (_board & (1 << c)) != 0)
				.Select(c => new Cell(c / 3, c % 3));
		}

		private IEnumerable<Cell> GetAwayMoves()
		{
			return Enumerable
				.Range(0, 9)
				.Where(c => (_board & (1 << c)) == 0 && (_occupied & (1 << c)) != 0)
				.Select(c => new Cell(c / 3, c % 3));
		}


		private void GetWinner()
		{
			foreach (var winningPosition in _winningPositions)
			{
				if ((_board & winningPosition) == winningPosition)
				{
					_isGameOver = true;
					Console.WriteLine("Player X wins");
				}

				if (((_board ^ _occupied) & winningPosition) == winningPosition)
				{
					_isGameOver = true;
					Console.WriteLine("Player O wins");
				}
			}
		}

		private IEnumerable<Line> GetWinningLines()
		{
			return _winningPositions
				.Where(winPos => (winPos & _board) == winPos || ((_board ^ _occupied) & winPos) == winPos)
				.Select(l =>
				{
					var binStr = new string(Convert.ToString(l, 2).PadLeft(9).Reverse().ToArray());
					var fstIndex = binStr.IndexOf('1');
					var lstIndex = binStr.LastIndexOf('1');
					return new Line(new Cell(fstIndex / 3, fstIndex % 3), new Cell(lstIndex / 3, lstIndex % 3));
				});
		}

		private static int Base3StringToInt(string base3)
		{
			int.TryParse(base3, out var ternary);

			if (ternary != 0)
			{
				var result = 0;
				var i = 0;

				while (ternary != 0)
				{
					var remainder = ternary % 10;
					ternary /= 10;
					result += remainder *(int) Math.Pow(3, i);
					++i;
				}

				return result;
			}
			else
				return 0;
		}
	}
}
