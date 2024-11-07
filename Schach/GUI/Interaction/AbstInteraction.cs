using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Schach.GUI;

namespace Schach
{
	internal abstract class AbstInteraction
	{
		public enum Type
		{
			None = 0,
			Highlight,
			Move,

		}


		public List<ChessPosition> listTiles;
		public ChessPosition positionOrigin;
		public Type mode { get; protected set; }

		protected AbstInteraction(ChessPosition origin, Type mode)
		{
			this.listTiles = new List<ChessPosition>();
			this.mode = mode;
			this.positionOrigin = origin;
		}

		public abstract void Draw(ref ChessBox chessBox, Board board);

		public void Undraw(ref ChessBox chessBox, Board board)
		{
			//chessBox.UpdateTile(positionOrigin, board.Field[positionOrigin.X, positionOrigin.Y]);


			foreach (ChessPosition pos in listTiles)
			{
				//Console.WriteLine("Undoing: " + this.mode.ToString() + " on " + pos.ToString());
				chessBox.UpdateTile(pos, board.Field[pos.X, pos.Y]);
				
			}

		}

	}
}
