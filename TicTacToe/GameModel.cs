using System;
using System.Linq;

namespace TicTacToe
{
    public class GameModel
    {
        StatesField[,] Game;
        public readonly int Size = 3;
        GameState StatusGame;
        MoveState OrderPlayerTurn;
        string Winner;

        public GameModel()
        {
            Game = new StatesField[Size, Size];
            StatusGame = GameState.InProcees;
            OrderPlayerTurn = MoveState.FirstPlayerMove;
        }

        public void Start()
        {
            for (int column = 0; column < Size; column++)
                for (int row = 0; row < Size; row++)
                    Game[column, row] = StatesField.EmptyField;
        }

        void SetState(int row, int column, StatesField state)
        {
            Game[row, column] = state;
            if (StateChanged != null) StateChanged(row, column, Game[row, column]);
        }

        public GameState Move(int row, int column)
        {
            if (StatusGame == GameState.IsOver)
                return GameState.IsOver;

            if (Game[row, column] == StatesField.EmptyField)
            {
                if (OrderPlayerTurn == MoveState.FirstPlayerMove)
                    SetState(row, column, StatesField.Cross);
                else
                    SetState(row, column, StatesField.Zero);
            }
            else
                return StatusGame;

            if (CheckWin())
            { 
                StatusGame = GameState.IsOver;
                Winner = OrderPlayerTurn == MoveState.FirstPlayerMove
                ? "First Player" : "Second Player";
            }

            if (CheckDraw())
            { 
                StatusGame = GameState.IsOver;
                Winner = "Draw";
            }

            OrderPlayerTurn = OrderPlayerTurn == MoveState.FirstPlayerMove
                ? MoveState.SecondPlayerMove : MoveState.FirstPlayerMove;
            return StatusGame;
        }

        private bool CheckDraw()
        {
            int count = 9;

            foreach (var field in Game)
            {
                if (field != StatesField.EmptyField)
                    count--;
            }

            return count == 0;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < Size; i++)
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
            return ( Game[0, 0] == Game[1, 1]
                && Game[1, 1] == Game[2, 2]
                && Game[1, 1] != StatesField.EmptyField)
                ||
                (  Game[0, 2] == Game[1, 1]
                && Game[1, 1] == Game[2, 0]
                && Game[1, 1] != StatesField.EmptyField) ;
        }

        private bool CheckRowe(int i)
        {
            return (
                Game[0, i] == Game[1, i]
                && Game[1, i] == Game[2, i]
                && Game[0, i] != StatesField.EmptyField
                );
        }

        private bool CheckLine(int i)
        {
            return (
                Game[i, 0] == Game[i, 1]
                && Game[i, 1] == Game[i, 2]
                && Game[i, 0] != StatesField.EmptyField
                );
        }

        public event Action<int, int, StatesField> StateChanged;

        public string GetGameStatus()
        {
            if (Winner == null)
                return "Game in process";
            if (Winner == "First Player" || Winner == "Second Player")
                return Winner + " Win";
            return "Draw";
        }
    }
}
