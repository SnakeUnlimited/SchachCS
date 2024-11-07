using Schach.Figuren;
using Schach.GUI;
using Schach.Intel;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schach
{
	public partial class FormChess : Form
	{

		public Game game;
		ChessBox chessBox;
		PromotionBox promoBox;

		#region Init Functions
		internal FormChess()
		{

			chessBox = new ChessBox();
			PromotionBox promoBox;

			//game = new Game();


			InitializeComponent();

		}

		

		private void FormChess_Load(object sender, EventArgs e)
		{


			this.Controls.Add(chessBox);
			this.Controls.Add(promoBox);
			chessBox.Click += new EventHandler(OnChessBoxClick);

		}

		

		private void btnStart_Click(object sender, EventArgs e)
		{


			Console.WriteLine("DEBUG1");
			game = new Game();
			game.board = new Board();

			/*
				EVENTS
			*/
			game.NewMoveEntry += OnNextMove;
			game.board.NewCheckEntry += OnCheck;
			game.board.NewCheckmateEntry += OnCheckmate;
			
			game.board.NewPromotionEntry += OnPromotion;


			game.Start(ref chessBox);
			this.infoCurrentPlayer.BorderStyle = BorderStyle.FixedSingle;
			this.infoCurrentPlayer.BackColor = Color.White;

			this.listMove.Items.Clear();

			this.infoCheck.BorderStyle = BorderStyle.FixedSingle;
			this.infoCheck.BackColor = Color.Green;


		}

		
		#endregion

		#region Private Methods
		private void SwapPlayer() {
			if (this.infoCurrentPlayer.BackColor == Color.White) {
				this.infoCurrentPlayer.BackColor = Color.Black;
			} else {
				this.infoCurrentPlayer.BackColor = Color.White;
			}
		}

		private void SwapCheck() {
			if (infoCheck.BackColor == Color.Green) {
				infoCheck.BackColor = Color.Red;
			} else {
				infoCheck.BackColor = Color.Green;
			}
		}

		private void KillKings() {
			var kings = game.board.GetAllPiecesByType(AbstPiece.Type.King);
			for (int i = 0; i < kings.Count; i++)
			{
				game.board.RemovePiece(AbstPiece.Type.King);
			}

			infoCheck.BackColor = Color.Red;
			infoCurrentPlayer.BackColor = Color.Red;
		}


		private void MoveListAdd(string move) {
			this.listMove.Items.Add(game.moveCounter.ToString() + ": " + move);
		}
		#endregion

		#region Events
		private void OnChessBoxClick(object? sender, EventArgs e)
		{
			game.OnClick(ref chessBox, ChessPosition.CoordsToChessPos(chessBox.PointToClient(Cursor.Position)));

		}

		public void OnNextMove(object? o, MoveEntryEventArgs e)
		{
			SwapPlayer();
			MoveListAdd(e.Move);

			game.board.NewPromotionEntry += OnPromotion;
			
		}



		private void OnCheck(object sender, CheckEntryEventArgs e)
		{
			SwapCheck();
		}

		private void OnCheckmate(object sender, MoveEntryEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void OnPromotion(object sender, PositionEventArgs e)
		{

			Console.WriteLine("Draw Promobox");
			promoBox = new PromotionBox(e.Position);
			promoBox.Location = new Point(500, 0);
			this.Controls.Add(promoBox);

			promoBox.NewPromotionSelectedEntry += game.board.OnPromotion;
			promoBox.NewPromotionSelectedEntry += OnPromotionSelected;

			// Freez Game
			chessBox.Click -= new EventHandler(OnChessBoxClick);

			promoBox.Draw();
			promoBox.Show();
		}

		private void OnPromotionSelected(object? sender, PieceEntryEventArgs e)
		{
			promoBox.Hide();
			Console.WriteLine("PROMO SELECTED" + e.Position.ToString());
		

			// Unfreeze Game
			chessBox.Click += new EventHandler(OnChessBoxClick);

		}


		#endregion
	}
}
