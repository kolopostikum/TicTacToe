namespace TicTacToe
{
    public class Player
    {
        readonly StatesField playerSymbol;

        public Player(StatesField symbol)
        {
            playerSymbol = symbol;
        }
        public void Move( int row, int column, GamePlace game)
        {
            game.ChangeFieldState(row, column, playerSymbol);
        }
    }
}
