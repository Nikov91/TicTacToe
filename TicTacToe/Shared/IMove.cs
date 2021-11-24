namespace TicTacToe.Shared
{
    public interface IMove
    {
        Cell Location { get; }
        IImmutableBoard Make();
    }
}
