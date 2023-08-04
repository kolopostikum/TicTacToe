using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe;

namespace TicTacToe
{
    class MyForm : Form
    {
        GameModel game;
        TableLayoutPanel table;

        public MyForm(GameModel game)
        {
			this.game = game;
			table = new TableLayoutPanel();

			table.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));

			for (int i = 0; i < game.Size; i++)
			{
				table.RowStyles.Add(new RowStyle(SizeType.Percent, 90f / game.Size));
				table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / game.Size));
			}

			TextBox textBox1 = new TextBox();
			textBox1.Dock = DockStyle.Fill;
			table.Controls.Add(textBox1, 0, 0);
			table.SetColumnSpan(textBox1, 3);

			for (int column = 0; column <= game.Size; column++)
				for (int row = 1; row <= game.Size; row++)
				{
					var iRow = row;
					var iColumn = column;
					var button = new Button();
					button.Dock = DockStyle.Fill;
					button.Click += (sender, args) => 
						{
							game.Move(iRow - 1, iColumn);
							textBox1.Text = game.GetGameStatus(); 
						};
					table.Controls.Add(button, iColumn, iRow);
				}

			table.Dock = DockStyle.Fill;
			Controls.Add(table);

			game.StateChanged += (row, column, state) =>
			{
				((Button)table.GetControlFromPosition(column, row + 1)).BackColor = state == StatesField.EmptyField ? Color.Black : Color.White;
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
			Application.Run(new MyForm(game) { ClientSize = new Size(300, 300) });
		}
	}
}
