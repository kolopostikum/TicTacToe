namespace TicTacToe
{
    public class GamePlace
    {
        readonly public int size = 3;

        public readonly StatesField[,] place;
        
        public GamePlace()
        {
            place = new StatesField[size, size];
        }

        public void MakeNewGamePlace() 
        {
            for (int column = 0; column < size; column++)
                for (int row = 0; row < size; row++)
                    place[column, row] = StatesField.EmptyField; ; 
        }

        public void ChangeFieldState(int row, int column, StatesField state) 
        {
            place[row, column] = state;
        }

        public StatesField GetStatesField(int row, int column)
        {
            return place[row, column];
        }
    }
}
