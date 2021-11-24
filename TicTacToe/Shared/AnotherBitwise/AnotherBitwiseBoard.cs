using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Shared.AnotherBitwise
{
    public class AnotherBitwiseBoard : IMutableBoard
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

        private const int PlayerX = 0b_000_000_001;
        private int _activePlayer = 0b_000_000_001;

        private int _board;
        private int _occupied;
        private bool _isGameOver;

        public IEnumerable<Cell> HomeMoves => Enumerable
          .Range(0, 9)
          .Where(c => (_board & (1 << c)) != 0)
          .Select(c => new Cell(c / Constants.Dimension, c % Constants.Dimension));

        public IEnumerable<Cell> AwayMoves => Enumerable
          .Range(0, 9)
          .Where(c => (_board & (1 << c)) == 0 && (_occupied & (1 << c)) != 0)
          .Select(c => new Cell(c / 3, c % 3));

        public IEnumerable<Cell> PlayableCells => Enumerable
          .Range(0, 9)
          .Where(c => (_occupied & (1 << c)) == 0 && !_isGameOver)
          .Select(c => new Cell(c / 3, c % 3));

        public IEnumerable<Line> WinningLines => _winningPositions
          .Where(winPos => (winPos & _board) == winPos || ((_board ^ _occupied) & winPos) == winPos)
          .Select(l =>
          {
              var binStr = new string(Convert.ToString(l, 2).PadLeft(9).Reverse().ToArray());
              var fstIndex = binStr.IndexOf('1');
              var lstIndex = binStr.LastIndexOf('1');
              return new Line(new Cell(fstIndex / 3, fstIndex % 3), new Cell(lstIndex / 3, lstIndex % 3));
          });

        public void Play(Cell cell)
        {
            var position = Base3StringToInt($"{cell.Row}{cell.Column}");
            _board |= _activePlayer << position;
            _occupied |= 0b_000_000_001 << position;
            CheckIfGameIsOver();
            _activePlayer = PlayerX ^ _activePlayer;
        }

        private void CheckIfGameIsOver()
        {
            foreach (var winningPosition in _winningPositions)
            {
                if ((_board & winningPosition) != winningPosition &&
                    ((_board ^ _occupied) & winningPosition) != winningPosition) continue;
                _isGameOver = true;
                break;
            }
        }

        private static int Base3StringToInt(string base3)
        {
            int.TryParse(base3, out var ternary);

            if (ternary == 0) return 0;
            var result = 0;
            var i = 0;

            while (ternary != 0)
            {
                var remainder = ternary % 10;
                ternary /= 10;
                result += remainder * (int)Math.Pow(3, i);
                ++i;
            }

            return result;

        }

        public int CountContinuations() => 0;
    }
}
