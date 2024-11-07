using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	internal class RuleKnight
	{
		internal static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			List<ChessPosition> validMoves = new List<ChessPosition>();

			Point[] positions = new Point[8];


			// TOP RIGHT
			positions[0] = new Point((byte)(positionPiece.X + 1), (byte)(positionPiece.Y + 2));

			// TOP LEFT
			positions[1] = new Point((byte)(positionPiece.X + 1), (byte)(positionPiece.Y - 2));

			// LEFT UP
			positions[2] = new Point((byte)(positionPiece.X + 2), (byte)(positionPiece.Y + 1));

			// LEFT DOWN
			positions[3] = new Point((byte)(positionPiece.X + 2), (byte)(positionPiece.Y - 1));

			// RIGHT UP
			positions[4] = new Point((byte)(positionPiece.X - 2), (byte)(positionPiece.Y + 1));

			// RIGHT DOWN
			positions[5] = new Point((byte)(positionPiece.X - 2), (byte)(positionPiece.Y - 1));

			// DOWN LEFT
			positions[6] = new Point((byte)(positionPiece.X - 1), (byte)(positionPiece.Y - 2));

			// DOWN RIGHT
			positions[7] = new Point((byte)(positionPiece.X - 1), (byte)(positionPiece.Y + 2));


			foreach (Point position in positions) {
				if (IsValidPosition(board, positionPiece, position, board.Field[positionPiece.X,positionPiece.Y].color)) {
					validMoves.Add(new ChessPosition((byte)position.X,(byte)position.Y));
				}
			}

			return validMoves;
		}

		private static bool IsValidPosition(Board board, ChessPosition positionPiece, Point position, AbstPiece.Color ownColor)
		{

			if (ChessPosition.IsOutOfBounce((byte)position.X, (byte)position.Y))
			{
				return false;
			}
			if (board.Field[position.X, position.Y] != null)
			{
				if (board.Field[position.X, position.Y].color == ownColor)
				{
					return false;
				}
			}
			return true;
		}

	
	}
}
