using Schach.Figuren;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
	public static class PieceHandler
	{

		public static AbstPiece GetPiece(AbstPiece.Type type, AbstPiece.Color color)
		{
			AbstPiece result;

			switch (type)
			{
				case AbstPiece.Type.Pawn:
					result = new Pawn(color);
					break;
				case AbstPiece.Type.Knight:
					result = new Knight(color);
					break;
				case AbstPiece.Type.Bishop:
					result = new Bishop(color);
					break;
				case AbstPiece.Type.Rook:
					result = new Rook(color);
					break;
				case AbstPiece.Type.Queen:
					result = new Queen(color);
					break;
				case AbstPiece.Type.King:
					result = new King(color);
					break;
				default:
					result = new King(color);
					break;
			}
			return result;
		}

		public static List<AbstPiece> GetPieces(AbstPiece.Color color)
		{
			List<AbstPiece> result = new List<AbstPiece>();

			result.Add(GetPiece(AbstPiece.Type.Pawn, color));
			result.Add(GetPiece(AbstPiece.Type.Knight, color));

			result.Add(GetPiece(AbstPiece.Type.Bishop, color));
			result.Add(GetPiece(AbstPiece.Type.Rook, color));


			result.Add(GetPiece(AbstPiece.Type.Queen, color));
			result.Add(GetPiece(AbstPiece.Type.King, color));

			return result;
		}


		public static AbstPiece[,] GetDefault() 
		{

			AbstPiece[,] pieceSet = new AbstPiece[Rulebook.BOARD_SIZE, Rulebook.BOARD_SIZE];

			// Pawns
			for (byte i = 0; i < 8; i++)
			{
				
				pieceSet[i, 1] = new Pawn(AbstPiece.Color.White);
				pieceSet[i, 6] = new Pawn(AbstPiece.Color.Black);
			}

			// Knights
			pieceSet[1, 0] = new Knight(AbstPiece.Color.White);
			pieceSet[6, 0] = new Knight(AbstPiece.Color.White);
			pieceSet[1, 7] = new Knight(AbstPiece.Color.Black);
			pieceSet[6, 7] = new Knight(AbstPiece.Color.Black);


			// Bishops
			pieceSet[2, 0] = new Bishop(AbstPiece.Color.White);
			pieceSet[5, 0] = new Bishop(AbstPiece.Color.White);
			pieceSet[2, 7] = new Bishop(AbstPiece.Color.Black);
			pieceSet[5, 7] = new Bishop(AbstPiece.Color.Black);

			// Rooks
			pieceSet[0, 0] = new Rook(AbstPiece.Color.White);
			pieceSet[7, 0] = new Rook(AbstPiece.Color.White);
			pieceSet[0, 7] = new Rook(AbstPiece.Color.Black);
			pieceSet[7, 7] = new Rook(AbstPiece.Color.Black);


			// Queens
			pieceSet[3, 0] = new Queen(AbstPiece.Color.White);
			pieceSet[3, 7] = new Queen(AbstPiece.Color.Black);

			// Kings
			pieceSet[4, 0] = new King(AbstPiece.Color.White);
			pieceSet[4, 7] = new King(AbstPiece.Color.Black);

			return pieceSet;
		}

		public static AbstPiece[,] GetTest() 
		{
			AbstPiece[,] pieceSet = new AbstPiece[Rulebook.BOARD_SIZE, Rulebook.BOARD_SIZE];

			// Knights
			pieceSet[1, 0] = new Knight(AbstPiece.Color.White);
			pieceSet[6, 7] = new Knight(AbstPiece.Color.Black);


			// Bishops
			pieceSet[2, 0] = new Bishop(AbstPiece.Color.White);
			pieceSet[5, 7] = new Bishop(AbstPiece.Color.Black);

			// Rooks
			pieceSet[0, 0] = new Rook(AbstPiece.Color.White);
			pieceSet[7, 7] = new Rook(AbstPiece.Color.Black);


			// Queens
			pieceSet[3, 0] = new Queen(AbstPiece.Color.White);
			pieceSet[3, 7] = new Queen(AbstPiece.Color.Black);

			// Kings
			pieceSet[4, 0] = new King(AbstPiece.Color.White);
			pieceSet[4, 7] = new King(AbstPiece.Color.Black);

			return pieceSet;
		}


	}
}