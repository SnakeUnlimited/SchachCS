using Schach.GUI;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Interaction
{
	internal class Highlight : AbstInteraction
	{


		public Highlight(ChessPosition origin, Type mode = Type.Highlight) : base(origin, mode)
		{
		}
		

		public bool HasClickedOnHighlight(ChessPosition position) 
		{
			foreach (ChessPosition positionTile in listTiles)
			{
				if (positionTile.X == position.X) {
					if (positionTile.Y == position.Y)
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool HasClickedOnOrigin(ChessPosition position) 
		{
			if (this.positionOrigin.X == position.X) {
				if (this.positionOrigin.Y == position.Y)
				{
					return true;
				}
			}
			return false;
		}

		public ChessPosition GetPositionClickedOn(ChessPosition position)
		{
			foreach (ChessPosition tile in listTiles)
			{
				if (tile.X == position.X)
				{
					if (tile.Y == position.Y)
					{
						ChessPosition pos = new ChessPosition(tile.X, tile.Y);
						pos.type = tile.type;
						return pos;
					}
				}
			}
			return position;
		}


		public override void Draw(ref ChessBox chessBox, Board board)
		{
			this.listTiles = Rulebook.GetAllLegalMoves(board, positionOrigin);

			foreach (ChessPosition position in listTiles)
			{
				chessBox.DrawDot(position);
			}
		}
	}
}
