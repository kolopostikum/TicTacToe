using System;
using System.Linq;

namespace TicTacToe
{

    public class GameModel
    {
        public readonly GamePlace gamePlace;
        private Player firstPlayer;
        private Player secondPlayer;
        public readonly GameStatus gameStatus;

        public GameModel()
        {
            gamePlace = new GamePlace();
            firstPlayer = new Player(StatesField.Cross);
            secondPlayer = new Player(StatesField.Zero);
            gameStatus = new GameStatus();
        }

        public void Start()
        {
            gamePlace.MakeNewGamePlace();
        }

        private void SetState(int row, int column, StatesField state)
        {
            gamePlace.ChangeFieldState(row, column, state); 
            if (StateChanged != null) StateChanged(row, column, gamePlace.GetStatesField(row, column));
        }

        public void Move(int row, int column)
        {
            if (gameStatus.GetGameStatus() == GameState.IsOver)
                return;

            if (gamePlace.GetStatesField(row, column) != StatesField.EmptyField)
                return;

            PlayerMove(row, column, gameStatus.GetOrderPlayerTurn());

            gameStatus.SetGameStatus(gamePlace);

            gameStatus.SetOrderPlayerTurn();
        }

        private void PlayerMove(int row, int column, MoveState orderPlayerTurn)
        {
            if (orderPlayerTurn == MoveState.FirstPlayerMove)
            {
                firstPlayer.Move(row, column, gamePlace);
                SetState(row, column, StatesField.Cross);
            }
            else
            {
                secondPlayer.Move(row, column, gamePlace);
                SetState(row, column, StatesField.Zero);
            }
        }

        public event Action<int, int, StatesField> StateChanged;
    }
}
