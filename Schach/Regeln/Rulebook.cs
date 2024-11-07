using Schach.Figuren;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach.Regeln
{
	internal static class Rulebook
	{

		public const byte BOARD_SIZE = 8;

		public const byte CASTLE_QUEEN_X = 2;
		public const byte CASTLE_KING_X = 6;



		/// <summary>
		/// All moves a piece does legally, therefore no checks or no moving awayy when pinned
		/// </summary>
		public static List<ChessPosition> GetAllLegalMoves(Board board, ChessPosition positionPiece)
		{
			// Nee nich so, dann checkt sich die dame selber 25.9
			List<ChessPosition> result = GetAllValidMoves(board, positionPiece);

			List<ChessPosition> resultNonCheck = new List<ChessPosition>();

			
			foreach (ChessPosition validPos in result) {
				if (RuleCheck.IsMoveLegal(board,positionPiece, validPos))
				{
					resultNonCheck.Add(validPos);
				}
			}

			return resultNonCheck;
		}

		public static bool IsCheckmate(Board board)
		{
			List<AbstPiece> pieceTypes = PieceHandler.GetPieces(board.CurrentPlayer);

			List<ChessPosition> piecePositions;

			List<ChessPosition> moves = new List<ChessPosition>();

			foreach (AbstPiece piece in pieceTypes)
			{
				piecePositions = board.GetAllPiecesByType(piece.type);
				foreach (var pos in piecePositions) {
					moves = GetAllLegalMoves(board, pos); 
				}
			}

			if (moves.Count == 0) {
				return true;
			}
			return false;
		}


		/// <summary>
		/// All moves a piece does, without check limitation (therefore with illegal moves)
		/// </summary>
		public static List<ChessPosition> GetAllValidMoves(Board board, ChessPosition positionPiece)
		{
			if (board.Field[positionPiece.X, positionPiece.Y] == null) {
				return new List<ChessPosition>();
			}


			//Console.WriteLine("Rulebook: GetAllValidMoves for " + board.Field[positionPiece.X, positionPiece.Y].type.ToString());
			List<ChessPosition> validMoves;
			switch (board.Field[positionPiece.X, positionPiece.Y].type)
			{
				case AbstPiece.Type.Pawn:
					validMoves = RulePawn.GetAllValidMoves(board, positionPiece);
					break;
				case AbstPiece.Type.Knight:
					validMoves = RuleKnight.GetAllValidMoves(board, positionPiece);
					break;

				case AbstPiece.Type.Bishop:
					validMoves = RuleBishop.GetAllValidMoves(board, positionPiece);
					break;
				case AbstPiece.Type.Rook:
					validMoves = RuleRook.GetAllValidMoves(board, positionPiece);
					break;
				case AbstPiece.Type.Queen:
					validMoves = RuleQueen.GetAllValidMoves(board, positionPiece);
					break;
				case AbstPiece.Type.King:
					validMoves = RuleKing.GetAllValidMoves(board, positionPiece);
					break;
				default:
					validMoves = new List<ChessPosition>();
					break;
			}


			return validMoves;
		}

		
		

		
	}
}
