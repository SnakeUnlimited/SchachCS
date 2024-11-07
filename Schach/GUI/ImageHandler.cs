using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.GUI
{
	public static class ImageHandler
	{
		public const string PATH = "Figuren/";
		public const string FORMAT = ".png";
		public const string WHITE = "White";
		public const string BLACK = "Black";



		public static Image GetImage(AbstPiece piece)
		{
			return Image.FromFile(PATH + piece.type.ToString() + (piece.color == AbstPiece.Color.White ? WHITE : BLACK) + FORMAT);
		}
	}
}
