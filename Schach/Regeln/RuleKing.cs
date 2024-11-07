using Schach.Figuren;
using Schach.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	internal class RuleKing
	{
		internal static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			List<ChessPosition> validMoves = new List<ChessPosition>();


			RoutineMoveNormal(board, positionPiece, ref validMoves);

			RoutineMoveCastle(board, positionPiece, ref validMoves);

			return validMoves;
		}

		private static void RoutineMoveCastle(Board board, ChessPosition positionPiece, ref List<ChessPosition> validMoves)
		{
			// Castling only when King not moved
			if (board.Field[positionPiece.X,positionPiece.Y].hasMoved) {
				return;
			}



			List<ChessPosition> rooks = board.GetAllPiecesByType(AbstPiece.Type.Rook);

			List<ChessPosition> tilesBetween;
			int deltaX;
			int direction;
			ChessPosition positionCastle;
			
			bool isCurrentDirectionPossible;

			foreach (ChessPosition positionRook in rooks)
			{
				
				// If same color
				if (board.Field[positionRook.X, positionRook.Y].color == board.Field[positionPiece.X,positionPiece.Y].color) {
					// If rook hasnt moved
					if (board.Field[positionRook.X, positionRook.Y].hasMoved == false)
					{
					//	Console.WriteLine("Castle? EACH ROOK TICK" + positionRook.ToString());
						// Get new king castle position
						deltaX = positionPiece.X - positionRook.X; // positive when king is more right, negative when more left
						direction = (deltaX >= 0 ? -1 : 1); // direction is left (-1) when king is more right

						positionCastle = new ChessPosition(positionPiece.X + (direction * 2), positionPiece.Y ); // King always moves 2 squares
						
						// trivial check?
						if (ChessPosition.IsOutOfBounce(positionCastle)) {
							continue;
						}


						// Check castle position
						
					


						isCurrentDirectionPossible = true;
						tilesBetween = Board.GetAllTilesInBetweenTwoPiecesInSameRow(positionPiece, positionRook);

						// Check each free tile between
						foreach (ChessPosition tile in tilesBetween)
						{
							
							// Is not free
							if (board.Field[tile.X, tile.Y] != null)
							{
							//	Console.WriteLine("Castle? Check: " + tile.ToString());
								isCurrentDirectionPossible = false;
							}

						}

						if (isCurrentDirectionPossible) {
						//	Console.WriteLine("Castle? Check: ADD" + positionCastle.ToString());
							positionCastle.type = ChessPosition.Type.Castle;
							validMoves.Add(positionCastle);
						}

						//Console.WriteLine("CastleCheck FINISHED------------------");
					}

				}
			}
		}



		private static void RoutineMoveNormal(Board board, ChessPosition positionPiece, ref List<ChessPosition> validMoves)
		{
			ChessPosition positionTarget;

			int targetX, targetY;

			// 3x3 Feld
			for (int x = -1; x < 2; x++)
			{

				for (int y = -1; y < 2; y++)
				{
					targetX = positionPiece.X + x;
					targetY = positionPiece.Y + y;

					positionTarget = new ChessPosition(targetX, targetY);

					// If Not Out of bounce
					if (!ChessPosition.IsOutOfBounce(targetX, targetY))
					{
						// If empty	
						if (board.Field[positionTarget.X, positionTarget.Y] == null)
						{
							//Console.WriteLine("RuleKing Adding empty field: " + positionTarget.ToString());
							validMoves.Add(positionTarget);
						}
						else


						// If not covered by teammate
						if (board.Field[positionTarget.X, positionTarget.Y].color != board.Field[positionPiece.X, positionPiece.Y].color)
						{
						// 	Console.WriteLine("PositionTarget: " + positionTarget.ToString());
						//	Console.WriteLine("Target Color: " + board.Field[positionTarget.X, positionTarget.Y].color.ToString());
						//	Console.WriteLine("Own Color: " + board.Field[positionPiece.X, positionPiece.Y].color.ToString());
						//	Console.WriteLine("RuleKing Adding enemy-occupied field: " + positionTarget.ToString());
							validMoves.Add(positionTarget);
						}


					}


				}

			}
		}
	}
}
