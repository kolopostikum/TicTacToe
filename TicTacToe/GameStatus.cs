namespace TicTacToe
{
    public class GameStatus
    {
        private GameState statusGame;
        private MoveState orderPlayerTurn;
        private string winner;

        public GameStatus()
        {
            statusGame = GameState.InProcees;
            orderPlayerTurn = MoveState.FirstPlayerMove;
        }

        public static bool CheckDraw(GamePlace game)
        {
            foreach (var field in game.place)
                if (field == StatesField.EmptyField)
                    return false;

            return true;
        }

        public static bool CheckWin(GamePlace game)
        {
            for (int i = 0; i < game.size; i++)
            {
                if (CheckLine(i, game.place) || CheckRowe(i, game.place))
                    return true;
            }
            if (CheckDiagonals(game.place))
                return true;
            return false;
        }

        private static bool CheckDiagonals(StatesField[,] place)
        {
            var diagonalLeftWin = (place[0, 2] == place[1, 1]
                && place[1, 1] == place[2, 0]
                && place[1, 1] != StatesField.EmptyField);

            var diagonalRightWin = (place[0, 0] == place[1, 1]
                && place[1, 1] == place[2, 2]
                && place[1, 1] != StatesField.EmptyField);

            return diagonalLeftWin || diagonalRightWin;
        }

        private static bool CheckRowe(int numberOfRow, StatesField[,] place)
        {
            return (
                place[0, numberOfRow] == place[1, numberOfRow]
                && place[1, numberOfRow] == place[2, numberOfRow]
                && place[0, numberOfRow] != StatesField.EmptyField
                );
        }

        private static bool CheckLine(int numberOfLine, StatesField[,] place)
        {
            return (
                place[numberOfLine, 0] == place[numberOfLine, 1]
                && place[numberOfLine, 1] == place[numberOfLine, 2]
                && place[numberOfLine, 0] != StatesField.EmptyField
                );
        }

        public string GetGameStatusToString()
        {
            if (winner == null)
                return "Game in process";
            if (winner == "First Player" || winner == "Second Player")
                return winner + " Win";
            return "Draw";
        }
        public GameState GetGameStatus()
        {
            return statusGame;
        }

        public void SetOrderPlayerTurn()
        {
            orderPlayerTurn = orderPlayerTurn == MoveState.FirstPlayerMove
            ? MoveState.SecondPlayerMove : MoveState.FirstPlayerMove;
        }

        public MoveState GetOrderPlayerTurn()
        {
            return orderPlayerTurn;
        }

        public void SetGameStatus(GamePlace game)
        {
            if (CheckWin(game))
            {
                statusGame = GameState.IsOver;
                winner = orderPlayerTurn == MoveState.FirstPlayerMove
                ? "First Player" : "Second Player";
            }
            else if (CheckDraw(game))
            {
                statusGame = GameState.IsOver;
                winner = "Draw";
            }
        }
    }
}
