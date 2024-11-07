using Schach.Figuren;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.GUI
{
	public class ChessBox : Panel
	{

		public Color BACKGROUND_WHITE = Color.White;
		public Color BACKGROUND_BLACK = Color.DarkGray;
		public const int SIZE = 420;

		public const int TILESIZE = 420 / Rulebook.BOARD_SIZE;
		public const int CIRCLE_SIZE = TILESIZE / 4;


		

		public ChessBox()
		{
			Size = new Size(SIZE, SIZE);
		}

		#region Update UpdateTile, UpdateBoard

		public void UpdateTile(ChessPosition positionTile, AbstPiece piece)
		{
			DrawTile(positionTile);
			
			if (piece != null)
			{
				DrawPiece(piece, positionTile);
			}

			// 
			if (positionTile.X == 0)
			{
				DrawNotationY(positionTile.Y);
			}

			if (positionTile.Y == 0) {
				DrawNotationX(positionTile.X);
			}
		}

		internal void UpdateBoard(Board board)
		{
			throw new NotImplementedException();
		}

		#endregion


		#region DrawDot, DrawCheckerboard, DrawTile , DrawPiece, DrawNotationX, DrawNotationY
		/// <summary>
		/// Draws a dot on Chess Square for highlighting possible moves
		/// </summary>
		/// <param name="position"></param>
		public void DrawDot(ChessPosition position)
		{
			Graphics gc = CreateGraphics();
			Pen pen = new Pen(Color.Chocolate);
			Point coords =
			ChessPosition.ChessPosToCoords(position);
			Rectangle rect = new Rectangle(coords.X, coords.Y, CIRCLE_SIZE, CIRCLE_SIZE);

			gc.DrawEllipse(pen, rect);
			gc.FillEllipse(new SolidBrush(Color.Chocolate), rect);
		}


		/// <summary>
		/// Draws the whole checkerboard
		/// </summary>
		private void DrawCheckers()
		{
			for (byte x = 0; x < Rulebook.BOARD_SIZE; x++)
			{
				for (byte y = 0; y < Rulebook.BOARD_SIZE; y++)
				{
					DrawTile(x, y);
				}
			}
		}


		public void DrawWholeBoard(Board board)
		{
			DrawCheckers();


			for (byte x = 0; x < Rulebook.BOARD_SIZE; x++)
			{
				for (byte y = 0; y < Rulebook.BOARD_SIZE; y++)
				{
					if (board.Field[x, y] != null)
					{
						DrawPiece(board.Field[x, y], new ChessPosition(x, y));
					}
				}

				DrawNotationX(x);
				DrawNotationY(x);
			}
		}

		private void DrawTile(ChessPosition position)
		{
			DrawTile(position.X, position.Y);
		}
		private void DrawTile(byte x, byte y) // Getting as ChessPosition
		{
			Graphics gc = CreateGraphics();
			y = (byte)(Rulebook.BOARD_SIZE - y - 1); // transforming to coords

			Rectangle rect = new Rectangle(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE);
			//rect = new Rectangle(TILESIZE * x, TILESIZE * y, TILESIZE, TILESIZE);

			Pen penWhite = new Pen(BACKGROUND_WHITE);
			Pen penBlack = new Pen(BACKGROUND_BLACK);

			Brush brushWhite = new SolidBrush(BACKGROUND_WHITE);
			Brush brushBlack = new SolidBrush(BACKGROUND_BLACK);

			if (x % 2 == 0)
			{
				if (y % 2 == 0)
				{
					gc.DrawRectangle(penWhite, rect);
					gc.FillRectangle(brushWhite, rect);
				}
				else
				{
					gc.DrawRectangle(penBlack, rect);
					gc.FillRectangle(brushBlack, rect);
				}
			}
			else
			{
				if (y % 2 == 0)
				{
					gc.DrawRectangle(penBlack, rect);
					gc.FillRectangle(brushBlack, rect);
				}
				else
				{
					gc.DrawRectangle(penWhite, rect);
					gc.FillRectangle(brushWhite, rect);
				}
			}
		}


		/// <summary>
		/// Draws a chess piece on position
		/// </summary>
		private void DrawPiece(AbstPiece piece, ChessPosition position)
		{
			Graphics gc = CreateGraphics();

			position.Y = (byte)(Rulebook.BOARD_SIZE - position.Y - 1);

			Rectangle rect = new Rectangle(position.X * TILESIZE, position.Y * TILESIZE, TILESIZE, TILESIZE);

			if (piece.color == AbstPiece.Color.White)
			{
				gc.DrawImage(ImageHandler.GetImage(piece), rect);
			}
			else
			{
				gc.DrawImage(ImageHandler.GetImage(piece), rect);
			}


		}

		private void DrawNotationY(byte chessY)
		{


			Graphics gc = CreateGraphics();

			Font font = new Font(ImageHandler.BLACK, 12, FontStyle.Bold);
			PointF pf = new PointF(0, TILESIZE * chessY);
			Brush brush = new SolidBrush(Color.Green);

			gc.DrawString((chessY + 1).ToString(), font, brush, pf);
		}


		private void DrawNotationX(byte chessX)
		{
			ChessPosition.ChessX enumChessX = (ChessPosition.ChessX)(chessX + 1);


			Graphics gc = CreateGraphics();

			Font font = new Font(ImageHandler.BLACK, 12, FontStyle.Bold);
			PointF pf = new PointF((int)chessX * TILESIZE + (TILESIZE / 2), SIZE - TILESIZE + (TILESIZE / 2));
			Brush brush = new SolidBrush(Color.Black);

			gc.DrawString(enumChessX.ToString(), font, brush, pf);
		}
		#endregion

	}
}
