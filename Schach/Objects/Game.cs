using Schach.GUI;
using Schach.Interaction;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Intel
{

	public class Game
	{


		public Board board;
		public int moveCounter { get; private set; }
		private Highlight? highlight;

		/*
			EVENTS
		*/
		public delegate void NewMoveEventHandler(object sender, MoveEntryEventArgs e);
		public event NewMoveEventHandler NewMoveEntry;

		public delegate void NewTileUpdateEventHandler(object sender, PieceEntryEventArgs e);
		public event NewTileUpdateEventHandler NewTileEntry;


		public Game() {
			//board = new Board();
		}


		public void Start(ref ChessBox chessBox) {
	//	/&/	highlight = new Highlight(new ChessPosition(0, 0));

			chessBox.DrawWholeBoard(board);
		}



		public void MovePiece(ref ChessBox chessBox, ChessPosition positionPiece, ChessPosition positionTarget) 
		{

			moveCounter++;

			ChessPosition highLightPosition = highlight.GetPositionClickedOn(positionTarget); // To gather Castle/EnPassantInfo


			if (NewMoveEntry != null) {
				NewMoveEntry(this, new MoveEntryEventArgs(RuleName.GetMoveName(board, positionPiece, highLightPosition)));
			}

			Board boardTemp = board.DoMove(positionPiece, highLightPosition, ref chessBox);

			this.board = new Board(boardTemp.Field, boardTemp.CurrentPlayer, boardTemp.LastMoveSource, boardTemp.LastMoveTarget);


			UnhighlightMoves(ref chessBox);
		}

		public void HighlightMoves(ref ChessBox chessBox, ChessPosition positionPiece) {

			UnhighlightMoves(ref chessBox);

			highlight = new Highlight(positionPiece);
			highlight.Draw(ref chessBox, board);
		}


		public void UnhighlightMoves(ref ChessBox chessBox) 
		{
			if (highlight == null) { return; }
			highlight.Undraw(ref chessBox, board);
			highlight = null;
		}


		public void OnClick(ref ChessBox chessBox, ChessPosition positionClick)
		{
		//	Console.WriteLine("Game: OnClick! " + positionClick.ToString());
		//	Console.WriteLine("Game: OnClick! " + board.LastMoveSource.ToString() + " -> " + board.LastMoveTarget.ToString());
			

			if (highlight != null)
			{

				
				if (highlight.HasClickedOnHighlight(positionClick))
				{
				//	Console.WriteLine("GameClick: Clicked into Highlighted; Origin= " + highlight.positionOrigin.ToString() + "; Clicked: "+positionClick.ToString());

					MovePiece(ref chessBox, highlight.positionOrigin, positionClick);
					return;
				}

				if (highlight.HasClickedOnOrigin(positionClick)) {
					//Console.WriteLine("GameClick: Clicked into Origin");


					UnhighlightMoves(ref chessBox);
					return;
				}

				
			}

			if (board.Field[positionClick.X, positionClick.Y] == null)
			{
				return;
			}

			// 
			if (board.Field[positionClick.X, positionClick.Y].color == board.CurrentPlayer)
			{
				HighlightMoves(ref chessBox, positionClick);
			}
		}

	}
}
