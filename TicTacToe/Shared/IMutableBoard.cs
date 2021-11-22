namespace TicTacToe.Shared
{
    public interface IMutableBoard : IBoard 
    { 
        void Play(Cell cell);
    }
}
