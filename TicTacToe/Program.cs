using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe;

namespace TicTacToe
{
    class TicTacToeForm : Form
    {
        private GameModel game;
        private TableLayoutPanel table;

        public TicTacToeForm(GameModel game)
        {
			this.game = game;
			table = new TableLayoutPanel();

			table.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));

			for (int i = 0; i < game.size; i++)
			{
				table.RowStyles.Add(new RowStyle(SizeType.Percent, 90f / game.size));
				table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / game.size));
			}

			TextBox textBox = new TextBox();
			textBox.Dock = DockStyle.Fill;
			table.Controls.Add(textBox, 0, 0);
			table.SetColumnSpan(textBox, 3);

			for (int column = 0; column <= game.size; column++)
				for (int row = 1; row <= game.size; row++)
				{
					var iRow = row;
					var iColumn = column;
					var button = new Button();
					button.Dock = DockStyle.Fill;
					button.Click += (sender, args) =>
						{
							game.Move(iRow - 1, iColumn);
							textBox.Text = game.GetGameStatus();
						};
					table.Controls.Add(button, iColumn, iRow);
				}

			table.Dock = DockStyle.Fill;
			Controls.Add(table);

			game.StateChanged += (row, column, state) =>
			{
				((Button)table.GetControlFromPosition(column, row + 1)).Text = state == StatesField.Cross ? "X" : "O";
			};

			game.Start();
		}

    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameModel();
            game.Start();
			Application.Run(new TicTacToeForm(game) { ClientSize = new Size(300, 300) });
		}
	}
}
