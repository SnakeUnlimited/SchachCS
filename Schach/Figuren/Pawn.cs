using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Figuren
{
	public class Pawn : AbstPiece
	{
		public Pawn(Color color, Type type = Type.Pawn) : base(color, type)
		{
		}

	}
}
