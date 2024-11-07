using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Schach.Regeln
{
	internal class RuleRook : RuleUnlimitedMovement
	{
		internal static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			List<ChessPosition> validMoves = new List<ChessPosition>();

			for (byte dir = 0; dir < 4; dir++) {
				RoutinePerDirection(board, positionPiece, dir, ref validMoves, RuleUnlimitedMovement.ROOKLIKE);
			}

			return validMoves;
		}


	}
}
