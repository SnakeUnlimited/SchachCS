using Schach.Figuren;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.GUI
{
	public class PromotionBox : Panel
	{

		public Color BACKGROUND = Color.Gold;
		public const int SIZE = 300;
		public const int TILESIZE = SIZE / Rulebook.BOARD_SIZE;


		public delegate void NewPromotionSelectedEventHandler(object sender, PieceEntryEventArgs e);
		public event NewPromotionSelectedEventHandler? NewPromotionSelectedEntry;

		public ChessPosition positionPromotion;

		public PromotionBox(ChessPosition position)
		{
			this.Size = new Size(TILESIZE, SIZE);

			this.Click += new EventHandler(OnClick);
			this.positionPromotion = position;

			Draw();
		}

		public void OnClick(object? sender, EventArgs e)
		{
			//Console.WriteLine("clicked promobox");
			if (NewPromotionSelectedEntry != null)
			{
				NewPromotionSelectedEntry(this, new PieceEntryEventArgs(GetPiece(), positionPromotion));
			}
		}


		public void Draw()
		{
			this.BackColor = Color.Green;

			List<AbstPiece> list = PieceHandler.GetPieces(AbstPiece.Color.White);

			byte counter = 0;

			foreach (AbstPiece piece in list) {
				if (piece.type == AbstPiece.Type.Pawn) {
					continue;
				}
				DrawPiece(piece, counter);
				counter++;
			}
		}

		public AbstPiece GetPiece() 
		{
			
			int increment = ChessBox.SIZE / Rulebook.BOARD_SIZE;
			int counter = 0;

			for (int tileSize = increment; tileSize < PromotionBox.SIZE; tileSize += increment)
			{
				if (this.PointToClient(Cursor.Position).Y < tileSize)
				{
					break;
					//return counter;
				}
				counter++;
			}

			return new Rook(AbstPiece.Color.White);
		}


		private void DrawPiece(AbstPiece piece, byte position)
		{
			Graphics gc = CreateGraphics();

			//position = (byte)(Rulebook.BOARD_SIZE - position - 1);



			Rectangle rect = new Rectangle(0, position * TILESIZE, TILESIZE, TILESIZE);
			gc.DrawImage(ImageHandler.GetImage(piece), rect);


		}
	}
}
