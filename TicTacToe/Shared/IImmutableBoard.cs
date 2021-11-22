namespace TicTacToe.Shared
{
    public interface IImmutableBoard : IBoard
    {
        IImmutableBoard Play(Cell cell);
    }
}
