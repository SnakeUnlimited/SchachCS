using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Figuren
{
	public abstract class AbstPiece
	{

		public enum Color {
			White = 0,
			Black = 1,
		}

		public enum Type {
			Pawn,
			Bishop,
			Knight,
			Rook,
			Queen,
			King,
			Empty,
		}

		public Color color { get; set; }
		public Type type { get; set; }
		public bool hasMoved { get; set; }

		public AbstPiece(Color color, Type type)
		{
			this.color = color;
			this.type = type;
			this.hasMoved = false;
		}

		public AbstPiece(AbstPiece piece)
		{
			this.color = piece.color;
			this.type = piece.type;
			this.hasMoved = piece.hasMoved;
		}

		public void Move() {
			this.hasMoved = true;
		}

		public override string ToString()
		{
			string result = type.ToString();
			result += " (" + color.ToString() + ") (Moved=" + hasMoved.ToString() + ")";

			return result;
		}
	}
}
