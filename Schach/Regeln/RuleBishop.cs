using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Schach.Regeln
{
	class RuleBishop : RuleUnlimitedMovement
	{
		static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			List<ChessPosition> validMoves = new List<ChessPosition>();

			for (byte i= 0; i < 4; i++) {
				RoutinePerDirection(board, positionPiece, i, ref validMoves, 
				RuleUnlimitedMovement.DIAGONAL);
			}

			return validMoves;
		}

	}
}
