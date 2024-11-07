using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	internal static class RuleCheck
	{


		public static bool IsMoveLegal(Board board, ChessPosition positionPiece, ChessPosition positionTarget) 
		{
			Board boardNew = board.DoMove(positionPiece, positionTarget);

			List<ChessPosition> posKings = boardNew.GetAllPiecesByType(AbstPiece.Type.King);

			foreach (ChessPosition posKing in posKings) {
				// Only illegal when checking OWN king
				if (boardNew.Field[posKing.X, posKing.Y].color == boardNew.Field[positionTarget.X, positionTarget.Y].color) {
					
					if (IsKingInCheck(boardNew, posKing)) {
						return false;
					}
				}
			}

			return true;
		}
		
		/// <summary>
		/// True when Player currently am zug is checked by enemy
		/// </summary>
		/// <param name="board"></param>
		/// <returns></returns>
		public static bool IsKingInCheck(Board board, ChessPosition posKing)
		{

			if (IsCheckLimitedPiece(board, AbstPiece.Type.Pawn, posKing)) {
				return true;
			}


			if (IsCheckLimitedPiece(board, AbstPiece.Type.Knight, posKing))
			{
				return true;
			}

			if (IsCheckUnlimitedPiece(board, AbstPiece.Type.Bishop, posKing))
			{
				return true;
			}


			if (IsCheckUnlimitedPiece(board, AbstPiece.Type.Rook, posKing))
			{
				return true;
			}
			return false; 
		}
		#region Private Methods
		private static bool IsCheckLimitedPiece(Board board, AbstPiece.Type type, ChessPosition posKing) 
		{
			RoutineSetKingTo(ref board, type, posKing);
			var validKingMoves = Rulebook.GetAllValidMoves(board, posKing);

			foreach (ChessPosition validMove in validKingMoves)
			{
				if (board.Field[validMove.X, validMove.Y] != null)
				{
					if (board.Field[validMove.X, validMove.Y].type == type)
					{
						if (board.Field[validMove.X, validMove.Y].color != board.Field[posKing.X, posKing.Y].color)
						{
							RoutineUnsetKingTo(ref board, posKing);
							return true;
						}
					}
				}
			}

			RoutineUnsetKingTo(ref board, posKing);

			return false;
		}

		private static bool IsCheckUnlimitedPiece(Board board, AbstPiece.Type type, ChessPosition posKing)
		{
			RoutineSetKingTo(ref board, type, posKing);
			var validKingMoves = Rulebook.GetAllValidMoves(board, posKing);

			foreach (ChessPosition validMove in validKingMoves)
			{
				if (board.Field[validMove.X, validMove.Y] != null)
				{
					if (board.Field[validMove.X, validMove.Y].type == type
					|| board.Field[validMove.X, validMove.Y].type == AbstPiece.Type.Queen) // No need for queen check when done like this
					{
						if (board.Field[validMove.X, validMove.Y].color != board.Field[posKing.X, posKing.Y].color)
						{
							RoutineUnsetKingTo(ref board, posKing);
							return true;
						}
					}
				}
			}
			RoutineUnsetKingTo(ref board, posKing);
			return false;
		}


		/// <summary>
		/// Setting KING to e.g. a bishop - then scan for enemy bishop -> check from bishop
		/// </summary>
		/// <param name="board"></param>
		/// <param name="pieceType"></param>
		/// <param name="positionKing"></param>
		/// <returns></returns>
		private static Board RoutineSetKingTo(ref Board board, AbstPiece.Type pieceType, ChessPosition positionKing) 
		{
			board.Field[positionKing.X, positionKing.Y] = PieceHandler.GetPiece(pieceType, board.Field[positionKing.X, positionKing.Y].color);
			return board;
		}
		private static Board RoutineUnsetKingTo(ref Board board, ChessPosition positionKing)
		{
			board.Field[positionKing.X, positionKing.Y] = PieceHandler.GetPiece(AbstPiece.Type.King, board.Field[positionKing.X, positionKing.Y].color);
			return board;
		}

		#endregion

	}
}
