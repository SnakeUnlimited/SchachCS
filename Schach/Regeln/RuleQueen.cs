using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	internal class RuleQueen
	{
		internal static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			List<ChessPosition> validMoves = new List<ChessPosition>();


			List<ChessPosition> validMovesBishop = RuleBishop.GetAllValidMoves(board, positionPiece);
			List<ChessPosition> validMovesRook = RuleRook.GetAllValidMoves(board, positionPiece);

			foreach (ChessPosition move in validMovesBishop) 
			{
				validMoves.Add(move);
			}

			foreach (ChessPosition move in validMovesRook) 
			{
				validMoves.Add(move);
			}

			return validMoves;
		}

	}
}
