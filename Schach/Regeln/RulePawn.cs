using Schach.Figuren;
using Schach.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	internal static class RulePawn
	{
		internal static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			List<ChessPosition> validMoves = new List<ChessPosition>();

			int directionFront; // Front differs from color

			if (board.Field[positionPiece.X, positionPiece.Y].color == AbstPiece.Color.White) 
			{
				directionFront = 1;
			} else {
				directionFront = -1;
			}


			RoutineSingleStep(board, positionPiece, ref validMoves, directionFront); // 1 step
			RoutineDoubleStep(board, positionPiece, ref validMoves, directionFront); // 2 steps

			RoutineEnPassant(board, positionPiece, ref validMoves);

			RoutineBeatDiagonal(board, positionPiece, ref validMoves, 1, directionFront); // Beat top left
			RoutineBeatDiagonal(board, positionPiece, ref validMoves,-1, directionFront); // Beat top right

			return validMoves;
		}


		private static void RoutineEnPassant(Board board, ChessPosition positionPiece, ref List<ChessPosition> validMoves)
		{
			// posPiece 3|4
			// LastMove 2|6 -> 2|4
			


			int deltaX = positionPiece.X - board.LastMoveTarget.X; // 1 or -1 hopefully


			// 1. Bedingung: ist lastPlayed gespielt x+-1 (daneben)
			if (Math.Abs(deltaX) == 1)
			{


				//Console.WriteLine("RoutineEnPassant: Move next to me..." + board.LastMove.source.ToString() + "->" + (Rulebook.BOARD_SIZE - board.LastMove.target.Y - 1).ToString());

				if (board.Field[board.LastMoveTarget.X, board.LastMoveTarget.Y] == null) { return;  }

				// 2. Bedingung: LastMove bauer?
				if (board.Field[board.LastMoveTarget.X , board.LastMoveTarget.Y].type == AbstPiece.Type.Pawn)
				{
					int deltaY, deltaStep;

					// deltaY anhand der Farbe
					if (board.Field[positionPiece.X, positionPiece.Y].color == AbstPiece.Color.White)
					{
						// white 4
						deltaY = 4;
						deltaStep = 1;
					}
					else
					{
						// black 3
						deltaY = 3;
						deltaStep = -1;
					}

					// 3. Bedingung: hat zwei nach vorne letze Runde gespielt
					if (Math.Abs(board.LastMoveTarget.Y - board.LastMoveSource.Y) >= 2)
					{

						// 4. Bedingung: Eigener Rank nur in der einen Reihe, wo EnPassant geht
						if (positionPiece.Y == deltaY) {
							// Passen Color des Gegner nicht nötig, da nur Fremde Color Züge gibt

							ChessPosition validMove = new ChessPosition(positionPiece.X - deltaX, positionPiece.Y + deltaStep);
							validMove.type = ChessPosition.Type.EnPassant;

							validMoves.Add(validMove);

						
						}
					}
				}
			}


		}


		private static void RoutineBeatDiagonal(Board board, ChessPosition positionPiece, ref List<ChessPosition> validMoves,int directionX, int directionY) 
		{
			

			// directionX = 1
			
			// Weiß 3|3 -> check 4|4 , 2|4
			// Weiß-> directionY = 1

			// Black 3|3 -> check 4|2, 2|2
			// Black-> directionY = -1

			int targetX = positionPiece.X + (directionX * 1);
			// weiß = 3 + 1 
			

			int targetY = positionPiece.Y + (directionY * 1);
			// weiß = 3 + 1
		

			ChessPosition positionTarget;


			// If not out of bounce
			if (!ChessPosition.IsOutOfBounce(targetX, targetY))
			{
				positionTarget = new ChessPosition(targetX, targetY);
				//Console.WriteLine("RulePawn: DiagonalCheck "+ positionTarget.ToString());
				// If not empty
				if (board.Field[positionTarget.X, positionTarget.Y] != null)
				{

					// If not teammate
					if (board.Field[positionPiece.X, positionPiece.Y].color != board.Field[positionTarget.X, positionTarget.Y].color) 
					{

						// Is Promoting Rank
						byte lastRank = (board.Field[positionPiece.X, positionPiece.Y].color == AbstPiece.Color.White ? (byte)7 : (byte)0);
						if (positionTarget.Y == lastRank)
						{
							positionTarget.type = ChessPosition.Type.Promotion;
						}


						//	Console.WriteLine("RulePawn: Adding Diagonal " + positionTarget.ToString());
						validMoves.Add(positionTarget);
					}
				}
			}
		}


		/// <param name="directionFront">1 for positive, -1 for negative</param>
		/// <param name="step">how many steps to front (over 1: only on not hasMoved</param>
		private static void RoutineSingleStep(Board board, ChessPosition positionPiece, ref List<ChessPosition> validMoves, int directionFront)
		{
			ChessPosition positionTarget = new ChessPosition(positionPiece.X, positionPiece.Y + (directionFront * 1));

			// If not out of bounce
			if (!ChessPosition.IsOutOfBounce(positionTarget))
			{
				// If empty
				if (board.Field[positionTarget.X, positionTarget.Y] == null)
				{
					// Is Promoting Rank
					byte lastRank = (board.Field[positionPiece.X, positionPiece.Y].color == AbstPiece.Color.White ? (byte)7 : (byte)0);
					if (positionTarget.Y == lastRank)
					{
						positionTarget.type = ChessPosition.Type.Promotion;
					}


					//	Console.WriteLine("RulePawn: Adding InFront " + positionTarget.ToString());
					validMoves.Add(positionTarget);
				}
			}
		}


		/// <param name="directionFront">1 for positive, -1 for negative</param>
		/// <param name="step">how many steps to front (over 1: only on not hasMoved</param>
		private static void RoutineDoubleStep(Board board, ChessPosition positionPiece, ref List<ChessPosition> validMoves, int directionFront) 
		{
			if (board.Field[positionPiece.X,positionPiece.Y].hasMoved) {
				return;
			}
			

			ChessPosition positionTarget = new ChessPosition(positionPiece.X, positionPiece.Y + (directionFront*2));
			ChessPosition positionBetween = new ChessPosition(positionPiece.X, positionPiece.Y + (directionFront * 1));

			// If not out of bounce (not for between, because it cant anyway)
			if (!ChessPosition.IsOutOfBounce(positionTarget)) 
			{
				// If empty target
				if (board.Field[positionTarget.X, positionTarget.Y] == null) 
				{
					// If empty between
					if (board.Field[positionBetween.X, positionBetween.Y] == null)
					{

					//	Console.WriteLine("RulePawn: Adding InFront " + positionTarget.ToString());
						// No promotion check aqquired
						validMoves.Add(positionTarget);
					}
				}
			}
		}

	}
}
