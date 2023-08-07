using System;
using System.Linq;

namespace TicTacToe
{
    public class GameModel
    {
        private StatesField[,] game;
        readonly public int size = 3;
        private GameState statusGame;
        private MoveState orderPlayerTurn;
        private string winner;

        public GameModel()
        {
            game = new StatesField[size, size];
            statusGame = GameState.InProcees;
            orderPlayerTurn = MoveState.FirstPlayerMove;
        }

        public void Start()
        {
            for (int column = 0; column < size; column++)
                for (int row = 0; row < size; row++)
                    game[column, row] = StatesField.EmptyField;
        }

        private void SetState(int row, int column, StatesField state)
        {
            game[row, column] = state;
            if (StateChanged != null) StateChanged(row, column, game[row, column]);
        }

        public GameState Move(int row, int column)
        {
            if (statusGame == GameState.IsOver)
                return GameState.IsOver;

            if (game[row, column] == StatesField.EmptyField)
            {
                if (orderPlayerTurn == MoveState.FirstPlayerMove)
                    SetState(row, column, StatesField.Cross);
                else
                    SetState(row, column, StatesField.Zero);
            }
            else
                return statusGame;

            if (CheckWin())
            { 
                statusGame = GameState.IsOver;
                winner = orderPlayerTurn == MoveState.FirstPlayerMove
                ? "First Player" : "Second Player";
            }
            else if (CheckDraw())
            { 
                statusGame = GameState.IsOver;
                winner = "Draw";
            }

            orderPlayerTurn = orderPlayerTurn == MoveState.FirstPlayerMove
                ? MoveState.SecondPlayerMove : MoveState.FirstPlayerMove;
            return statusGame;
        }

        private bool CheckDraw()
        {
            int count = 9;

            foreach (var field in game)
            {
                if (field != StatesField.EmptyField)
                    count--;
            }

            return count == 0;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < size; i++)
            {
                if (CheckLine(i) || CheckRowe(i))
                    return true;
            }
            if (CheckDiagonals())
                return true;
            return false;
        }

        private bool CheckDiagonals()
        {
            var diagonalLeftWin = (game[0, 2] == game[1, 1]
                && game[1, 1] == game[2, 0]
                && game[1, 1] != StatesField.EmptyField);
            
            var diagonalRightWin = (game[0, 0] == game[1, 1]
                && game[1, 1] == game[2, 2]
                && game[1, 1] != StatesField.EmptyField);

            return diagonalLeftWin || diagonalRightWin;
        }

        private bool CheckRowe(int numberOfRow)
        {
            return (
                game[0, numberOfRow] == game[1, numberOfRow]
                && game[1, numberOfRow] == game[2, numberOfRow]
                && game[0, numberOfRow] != StatesField.EmptyField
                );
        }

        private bool CheckLine(int numberOfLine)
        {
            return (
                game[numberOfLine, 0] == game[numberOfLine, 1]
                && game[numberOfLine, 1] == game[numberOfLine, 2]
                && game[numberOfLine, 0] != StatesField.EmptyField
                );
        }

        public event Action<int, int, StatesField> StateChanged;

        public string GetGameStatus()
        {
            if (winner == null)
                return "Game in process";
            if (winner == "First Player" || winner == "Second Player")
                return winner + " Win";
            return "Draw";
        }
    }
}
