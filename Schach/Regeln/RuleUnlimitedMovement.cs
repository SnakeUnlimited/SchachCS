
using Schach.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	public class RuleUnlimitedMovement
	{
		internal static void RoutinePerDirection(Board board, ChessPosition positionPiece, byte currentDirection, ref List<ChessPosition> validMoves, int[,] directions)
		{
			Point pointTarget = new Point(0, 0);

			for (int i = 1; i < Rulebook.BOARD_SIZE; i++)
			{
				pointTarget = new Point(positionPiece.X + (i * directions[currentDirection, 0]), positionPiece.Y + (i * directions[currentDirection, 1]));

				// Wenn Punkt out of map -> Diese Richtung fertig
				if (ChessPosition.IsOutOfBounce((byte)pointTarget.X, (byte)pointTarget.Y))
				{
					break;
				}

				// Wenn Feld nicht leer (danach fertig)
				if (board.Field[pointTarget.X, pointTarget.Y] != null)
				{
					// Wenn Teammates nicht verstopfen
					if (board.Field[pointTarget.X, pointTarget.Y].color != board.Field[positionPiece.X, positionPiece.Y].color)
					{
						validMoves.Add(new ChessPosition(pointTarget.X, pointTarget.Y));
					}

					break;
				}

				// Wenn Feld leer
				validMoves.Add(new ChessPosition((byte)pointTarget.X, (byte)pointTarget.Y));

			}
		}

		public static int[,] DIAGONAL = GetDirectionDiagonal();

		private static int[,] GetDirectionDiagonal() 
		{
			int[,] directions = new int[4, 2];
			directions[0, 0] = 1;
			directions[0, 1] = 1;

			directions[1, 0] = -1;
			directions[1, 1] = -1;

			directions[2, 0] = 1;
			directions[2, 1] = -1;


			directions[3, 0] = -1;
			directions[3, 1] = 1;
			return directions;
		}

		public static int[,] ROOKLIKE = GetDirectionRooklike();
		private static int[,] GetDirectionRooklike() {
			int[,] directions = new int[4, 2];
			directions[0, 0] = 1;
			directions[0, 1] = 0;

			directions[1, 0] = -1;
			directions[1, 1] = 0;

			directions[2, 0] =0;
			directions[2, 1] = 1;


			directions[3, 0] = 0;
			directions[3, 1] = -1;
			return directions;
		}
	}
}
