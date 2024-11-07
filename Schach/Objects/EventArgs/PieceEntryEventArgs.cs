using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    public class PieceEntryEventArgs : EventArgs
    {
        private readonly AbstPiece piece;
        private readonly ChessPosition position;

        public PieceEntryEventArgs(AbstPiece piece, ChessPosition position)
		{
			this.piece = piece;
			this.position = position;
		}

		public AbstPiece Piece
        {
            get { return this.piece; }
        }

        public ChessPosition Position
        {
            get { return this.position; }
        }
    }
}