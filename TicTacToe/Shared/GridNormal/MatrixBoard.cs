using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Shared.GridNormal
{
    public class MatrixBoard : IMutableBoard
    {
        private readonly int[][] winCombinations = new int[][]
        {
            new int[] {0, 1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},
            new int[] {0, 3, 6},
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},
            new int[] {0, 4, 8},
            new int[] {2, 4, 6}
        };
        public enum Position
        {
            None,
            X,
            O
        }

        public enum GameState
        {
            Running,
            Finished
        }

        private Position[][] board;
        private Position currentPlayer;
        private GameState state;
        private int[] winCombination;

        public MatrixBoard()
        {
            currentPlayer = Position.X;
            board = GenerateEmptyBoard();
            state = GameState.Running;
            winCombination = null;
        }

        private Position[][] GenerateEmptyBoard()
        {
            // lazy mans board :)
            return new Position[][] {
                new Position[] { Position.None, Position.None, Position.None },
                new Position[] { Position.None, Position.None, Position.None },
                new Position[] { Position.None, Position.None, Position.None },
                };
        }

        private bool IsInsideTheBoard(int x, int y) => x >= 0 && x < 3 && y >= 0 && y < 3;
        private bool IsFree(int x, int y) => board[x][y] == Position.None;
        private void SwitchTurn() => currentPlayer = currentPlayer == Position.X ? Position.O : Position.X;
        private bool AreAllFieldsTaken => board.All(r => r.All(c => c != Position.None));

        public IEnumerable<Cell> HomeMoves => this.GetCells(Position.X);

        public IEnumerable<Cell> AwayMoves => this.GetCells(Position.O);

        public IEnumerable<Cell> PlayableCells => this.GetCells(Position.None);

        private IEnumerable<Cell> GetCells(Position containing)
        {
            for (int row = 0; row < this.board.Length; row += 1)
                for (int col = 0; col < this.board[row].Length; col += 1)
                    if (this.board[row][col] == containing)
                        yield return new Cell(row, col);
        }

        public IEnumerable<Line> WinningLines =>
            this.GetLineCoords() is (int fromRow, int fromCol, int toRow, int toCol)
                ? new[] { new Line(new Cell(fromRow, fromCol), new Cell(toRow, toCol)) }
                : Enumerable.Empty<Line>();

        private int[] GetWinCombination()
        {
            var flattenBoard = board.SelectMany(b => b).ToArray();
            return winCombinations.FirstOrDefault((c) => c.All(index => flattenBoard[index] == currentPlayer));
        }

        private (int fromRow, int fromCol, int toRow, int toCol)? GenerateWinLine(int[] winCombo)
        {
            if (winCombo == null)
                return null;
            var from = winCombo[0];
            var to = winCombo[2];
            return (from / 3, from % 3, to / 3, to % 3);
        }

        public Position GetCell(int x, int y)
        {
            if (!IsInsideTheBoard(x, y))
                throw new Exception("Invalid Cell");

            return board[x][y];
        }

        public void PlaceMark(int x, int y)
        {
            if (state != GameState.Finished && IsInsideTheBoard(x, y) && IsFree(x, y))
            {
                board[x][y] = currentPlayer;
                var winCombo = GetWinCombination();
                if (winCombo != null) // currentPlayer won
                {
                    Console.WriteLine($"{currentPlayer} won");
                    winCombination = winCombo;
                    state = GameState.Finished;
                    return;
                }
                else if (AreAllFieldsTaken) // tie
                {
                    Console.WriteLine($"{currentPlayer} won");
                    state = GameState.Finished;
                }

                SwitchTurn();
            }
        }

        public (int fromRow, int fromCol, int toRow, int toCol)? GetLineCoords() => GenerateWinLine(winCombination);

        public void Play(Cell cell) => this.PlaceMark(cell.Row, cell.Column);

        public int CountContinuations() => 0;
    }
}
