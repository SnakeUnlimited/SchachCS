using Schach.Figuren;
using Schach.GUI;
using Schach.Intel;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Schach.ChessPosition;
using Timer = System.Windows.Forms.Timer;

namespace Schach
{
	public class Board
	{

		public AbstPiece.Color CurrentPlayer { get; private set; }
		public ChessPosition LastMoveSource { get; set; }
		public ChessPosition LastMoveTarget { get; set; }
		public AbstPiece[,] Field { get; set; }


		public delegate void NewPromotionEventHandler(object sender, PositionEventArgs e);
		public event NewPromotionEventHandler NewPromotionEntry;

		public delegate void NewCheckEventHandler(object sender, CheckEntryEventArgs e);
		public event NewCheckEventHandler NewCheckEntry;

		public delegate void NewCheckmateEventHandler(object sender, MoveEntryEventArgs e);
		public event NewCheckmateEventHandler NewCheckmateEntry;

		public bool IsInCheck
		{
			get
			{
				if (RuleCheck.IsKingInCheck(this, this.GetAnyPieceByTypeAndColor(AbstPiece.Type.King, CurrentPlayer)))
				{
					return true;
				}
				return false;
			}
		}



		#region Konstruktor
		public Board()
		{
			this.LastMoveSource = new ChessPosition();
			this.LastMoveTarget = new ChessPosition();

			this.CurrentPlayer = AbstPiece.Color.White;
			this.Field = PieceHandler.GetDefault();

			//&//this.Field = PieceHandler.GetTest();


		}

		public Board(AbstPiece[,] field, AbstPiece.Color currentPlayer, ChessPosition lastMoveSource, ChessPosition lastMoveTarget)
		{
			this.LastMoveSource = new ChessPosition();
			this.LastMoveTarget = new ChessPosition();
			this.CurrentPlayer = currentPlayer;
			this.Field = field;
			this.LastMoveSource = lastMoveSource;
			this.LastMoveTarget = lastMoveTarget;

		}
		#endregion

		#region DoMove()

		public Board DoMove(ChessPosition source, ChessPosition target)
		{
			Board boardNew = new Board();

			boardNew.CurrentPlayer = (CurrentPlayer == AbstPiece.Color.White ? AbstPiece.Color.Black : AbstPiece.Color.White);

			//Console.WriteLine("Board: LastMove " + LastMoveSource.ToString() + " -> " + LastMoveTarget.ToString());
			//Console.WriteLine("Board: Next Lastmove will be " + source.ToString() + " -> " + target.ToString());

			// COPY
			for (byte x = 0; x < Rulebook.BOARD_SIZE; x++)
			{
				for (byte y = 0; y < Rulebook.BOARD_SIZE; y++)
				{
					boardNew.Field[x, y] = Field[x, y];

				}
			}


			//Console.WriteLine("Board.DoMove: Fired " + target.type.ToString());
			switch (target.type)
			{
				case ChessPosition.Type.EnPassant:
					boardNew.Field[LastMoveTarget.X, LastMoveTarget.Y] = null;
					break;
				case ChessPosition.Type.Castle:

					byte rank = (boardNew.Field[source.X, source.Y].color == AbstPiece.Color.White ? (byte)0 : (byte)7);

					// King castle
					if (source.X < target.X)
					{
						boardNew.Field[7, rank] = null;

						boardNew.Field[5, rank] = new Rook(boardNew.Field[source.X, source.Y].color);
						// Queen Castle
					}
					else
					{
						boardNew.Field[0, rank] = null;

						boardNew.Field[3, rank] = new Rook(boardNew.Field[source.X, source.Y].color);
					}
					break;

				case ChessPosition.Type.Promotion:
					// welche figur willstn habn?? -> wichtig für check?!
					
			//	>//	NewPromotionEntry?.Invoke(this, new PromotionEntryEventArgs(target));
					break;
			}



			boardNew.Field[target.X, target.Y] = Field[source.X, source.Y];
			boardNew.Field[source.X, source.Y] = null;
			//boardNew.Field[target.X, target.Y].hasMoved = true;


			// Update Last Move
			boardNew.LastMoveSource = new ChessPosition(source.X, source.Y);
			boardNew.LastMoveTarget = new ChessPosition(target.X, Rulebook.BOARD_SIZE - target.Y - 1); // TODO WARUM IST DER Y VOM TARGET INVERTIERT?!?!?!?
			boardNew.NewPromotionEntry = this.NewPromotionEntry;



			return boardNew;
		}


		public void OnPromotion(object? sender, PieceEntryEventArgs e) 
		{
			Console.WriteLine("Heheh" + e.Piece.type.ToString());
			Field[e.Position.X, e.Position.Y] = e.Piece;

		}

		public Board DoMove(ChessPosition source, ChessPosition target, ref ChessBox chessBox) 
		{

			Board boardNew = new Board();

			boardNew.CurrentPlayer = (CurrentPlayer == AbstPiece.Color.White ? AbstPiece.Color.Black : AbstPiece.Color.White);

		//	Console.WriteLine("Board: LastMove " + LastMoveSource.ToString() + " -> " + LastMoveTarget.ToString());
			

			// COPY
			for (byte x = 0; x < Rulebook.BOARD_SIZE; x++) {
				for (byte y = 0; y < Rulebook.BOARD_SIZE; y++) {
					boardNew.Field[x, y] = Field[x, y];
					
				}
			}


			boardNew.Field[target.X, target.Y] = Field[source.X, source.Y];
			boardNew.Field[target.X, target.Y].hasMoved = true;
			chessBox.UpdateTile(target, boardNew.Field[target.X, target.Y]);

			target.Y = (byte)(7 - target.Y); // WARUM!?=!??!?!?!!?!?!?!?!??!?!ß!ß Muss das so sxein, warum invertiert er NUR DAS Y NUR VOM TARGET!? Asi -.-

			//Console.WriteLine("Board: Next Lastmove will be " + source.ToString() + " -> " + target.ToString());


			boardNew.Field[source.X, source.Y] = null;
			chessBox.UpdateTile(source, null);


			//Console.WriteLine("Board.DoMove: Fired " + target.type.ToString());
			switch (target.type)
			{
				case ChessPosition.Type.EnPassant:
					boardNew.Field[LastMoveTarget.X, LastMoveTarget.Y] = null;
					chessBox.UpdateTile(LastMoveTarget, null);
					break;
				case ChessPosition.Type.Castle:

					byte rank = (boardNew.Field[source.X, source.Y].color == AbstPiece.Color.White ? (byte)0 : (byte)7);
					
					// King castle
					if (source.X < target.X) {
						boardNew.Field[7, rank] = null;
						chessBox.UpdateTile(new ChessPosition((byte)7, rank), null);

						boardNew.Field[5, rank] = new Rook(boardNew.Field[source.X, source.Y].color);
						chessBox.UpdateTile(new ChessPosition((byte)5, rank), boardNew.Field[5, rank]);
					// Queen Castle
					} else {
						boardNew.Field[0, rank] = null;
						chessBox.UpdateTile(new ChessPosition((byte)0, rank), null);

						boardNew.Field[3, rank] = new Rook(boardNew.Field[source.X, source.Y].color);
						chessBox.UpdateTile(new ChessPosition((byte)3, rank), boardNew.Field[3, rank]);
					}
					break;

				case ChessPosition.Type.Promotion:
						Console.WriteLine("PROMOTION DETECTED");
					if (NewPromotionEntry != null)
					{
						NewPromotionEntry(this, new PositionEventArgs(target));
					}
					else
					{
						Console.WriteLine("No promo listener ");
					}
					break;
				case ChessPosition.Type.Check:
					NewCheckEntry(this, new CheckEntryEventArgs(CurrentPlayer));
					
					if (Rulebook.IsCheckmate(boardNew)) {
						NewCheckmateEntry(this, new MoveEntryEventArgs("Checkmate"));
					}

					break;

			}


			// Update Last Move
			boardNew.LastMoveSource = new ChessPosition(source.X, source.Y);
			boardNew.LastMoveTarget = new ChessPosition(target.X,target.Y); // TODO WARUM IST DER Y VOM TARGET INVERTIERT?!?!?!?


			return boardNew;
		}
		#endregion

		public void RemovePiece(AbstPiece.Type type) 
		{
			for (int x = 0; x < Rulebook.BOARD_SIZE; x++)
			{
				for (int y = 0; y < Rulebook.BOARD_SIZE; y++)
				{
					if (ChessPosition.IsOutOfBounce(x,y)) {
						if (Field[x,y].type == type)
						{
							Field[x, y] = null;
						}
					}
				}
			}
		}


		public List<ChessPosition> GetAllPiecesByType(AbstPiece.Type type)
		{
			List<ChessPosition> positions = new List<ChessPosition>();

			for (byte x = 0; x < Rulebook.BOARD_SIZE; x++)
			{
				for (byte y = 0; y < Rulebook.BOARD_SIZE; y++)
				{
					if (Field[x,y] != null) {
						if (Field[x,y].type == type) {
							positions.Add(new ChessPosition(x,y));
						}
					}
				}
			}
			return positions;

		}

		public List<ChessPosition> GetAllPiecesByTypeAndColor(AbstPiece.Type type, AbstPiece.Color color)
		{
			List<ChessPosition> positions = new List<ChessPosition>();

			foreach (ChessPosition position in GetAllPiecesByType(type))
			{
				if (Field[position.X,position.Y].color == color)
				{
					positions.Add(position);
				}
			}

			return positions;

		}

		public ChessPosition GetAnyPieceByTypeAndColor(AbstPiece.Type type, AbstPiece.Color color)
		{
			ChessPosition positions = new ChessPosition();

			foreach (ChessPosition position in GetAllPiecesByType(type))
			{
				if (Field[position.X, position.Y].color == color)
				{
					positions = new ChessPosition(position.X, position.Y);
					return positions;
				}
			}

			return positions;

		}

		public static List<ChessPosition> GetAllTilesInBetweenTwoPiecesInSameRow(ChessPosition piece1, ChessPosition piece2)
		{
			List<ChessPosition> tiles = new List<ChessPosition>();

			if (piece1.Y != piece2.Y) {
				return tiles;
			}


			int deltaX = piece1.X - piece2.X - 1; // positive when piece1 is more right, negative when more left
			int direction = (deltaX >= 0 ? -1 : 1); // direction is left when he is more right 

			/*	piece1.X = 4;
			 * deltaX = 4 -> Queensidecastle
			 * direction = -1 -> könig muss nach links
			 *	
			 * deltaX = -3 -> Kingsidecastle
			 * direction = 1 -> könig muss nach rechts
			 */

			List<int> list1 = GetPositionsInBetween(piece1.X, piece2.X);

			foreach (int pos in list1) {
				tiles.Add(new ChessPosition(pos, piece1.Y));
			}


			return tiles;
		}

		private static List<int> GetPositionsInBetween(int x1, int x2) 
		{
			/*
			 *	x1,x2 sind IMMER zwischen 0 und 7 
			 *  
			 *  Wenn z.B.
			 *  x1 = 3,
			 *  x2 = 7, 
			 *  ist, will ich in die Liste:
			 *  posList = { 4,5,6 }
			 *  
			 *  
			 *  Wenn z.B. 
			 *  x1 = 6,
			 *  x2 = 1,
			 *  Dann
			 *   posList = { 2,3,4,5 }
			 *   
			 *   Reihenfolge der posList egal!
			 *  
			 */
			List<int> posList = new List<int>();


			int newX1, newX2;

			if (x1 < x2) {
				newX1 = x1;
				newX2 = x2;
			} else {
				newX1 = x2;
				newX2 = x1;
			}

			for (int i = newX1 + 1; i < newX2; i++) {
			posList.Add(i);
			}

			return posList;
		}
		
	}
}
