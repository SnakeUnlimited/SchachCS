using Schach.GUI;
using Schach.Regeln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
	public class ChessPosition
	{
		// Byte 8 Bit, nur 6 bits gebraucht für posi. Farbe noch reinmachen?


		public enum Type
		{
			None = 0,
			EnPassant,
			Castle,
			Check,
			Promotion,
		}


		public Type type { get; set; }

		public enum ChessX
		{
			A = 1,
			B = 2,
			C = 3,
			D = 4,
			E = 5,
			F = 6,
			G = 7,
			H = 8,
		}


		/// <summary>
		/// Stores both X& Y of Chess position AND special rule - do not mess around
		/// </summary>
		private byte _value;

		#region Constructor



		/// <summary>
		/// Positions between 0 and 7
		/// </summary>
		/// <param name="x">Rows (White starts at X=0, Black X=7</param>
		/// <param name="y">Columns (A&H aka 0&7 got Rocks</param>
		public ChessPosition(byte x, byte y)
		{
			SetPosition(x, y);
		}
		public ChessPosition()
		{
			_value = 0;
		}

		public ChessPosition(int x, int y) 
		{
			if (x >= 0) {
				if (y >= 0) {
					SetPosition((byte)x, (byte)y);
				}
			}
		}


		#endregion

		#region Felder

		public byte X
		{
			get
			{
				return GetPositionX();
			}
			set
			{
				SetPositionX(value);
			}
		}

		public byte Y
		{
			get
			{
				return GetPositionY();
			}
			set
			{
				SetPositionY(value);
			}
		}
		#endregion


		#region Getter/Setter Position

		public byte GetPosition(bool isX)
		{
			if (isX)
			{
				return GetPositionX();
			}
			return GetPositionY();
		}

		public byte GetPositionX()
		{

			byte partX = (byte)(_value % 8);
			return partX;
		}

		public byte GetPositionY()
		{

			byte partX = GetPositionX();
			byte partY = (byte)((_value - partX) / 8);

			return partY;
		}


		public void SetPositionX(byte x)
		{
			byte tempY = GetPositionY();
			SetPosition(x, tempY);
		}
		public void SetPositionY(byte y)
		{
			byte tempX = GetPositionX();
			SetPosition(tempX, y);
		}

		public void SetPosition(byte x, byte y)
		{
			byte valueAdd = 0;

			_value = 0;
			_value += x;
			_value += (byte)(8 * y);
			_value += valueAdd;
		}
		#endregion


		#region Static Methods (IsOutOfBounce),  ChessPosToCoords, CoordsToChessPos

		/// <summary>
		/// To center
		/// </summary>
		public static Point ChessPosToCoords(ChessPosition position)
		{
			Point point = new Point();

			point.X = (position.X + 1) * ChessBox.TILESIZE;
			point.X -= ChessBox.TILESIZE / 2;
			point.X -= ChessBox.CIRCLE_SIZE / 2;
			point.Y = ChessBox.SIZE - (position.Y * ChessBox.TILESIZE);
			point.Y -= ChessBox.TILESIZE / 2;
			point.Y -= ChessBox.CIRCLE_SIZE / 2;

			return point;
		}

		public static ChessPosition CoordsToChessPos(Point relativeMousePos)
		{

			ChessPosition chessPosition = new ChessPosition();

			int increment = ChessBox.SIZE / Rulebook.BOARD_SIZE;
			int counterX = 0;
			int counterY = Rulebook.BOARD_SIZE;

			for (int tileSize = increment; tileSize < ChessBox.SIZE; tileSize += increment)
			{
				if (relativeMousePos.X < tileSize)
				{
					chessPosition.X = (byte)counterX;
					break;
				}
				counterX++;
			}

			for (int tileSize = increment; tileSize < ChessBox.SIZE; tileSize += increment)
			{
				counterY--;
				if (relativeMousePos.Y < tileSize)
				{
					chessPosition.Y = (byte)counterY;
					break;
				}
			}
			return chessPosition;
		}

		public static bool IsOutOfBounce(byte number)
		{
			if (number < 0) { return true; }
			if (number >= 8) { return true; }

			return false;
		}

		public static bool IsOutOfBounce(ChessPosition chessPosition)
		{
			return IsOutOfBounce(chessPosition.X, chessPosition.Y);
		}
		public static bool IsOutOfBounce(byte number1, byte number2)
		{
			if (IsOutOfBounce(number1))
			{
				return true;
			}
			if (IsOutOfBounce(number2))
			{
				return true;
			}
			return false;
		}

		public static bool IsOutOfBounce(int number1, int number2)
		{
			if (IsOutOfBounce(number1))
			{
				return true;
			}
			if (IsOutOfBounce(number2))
			{
				return true;
			}
			return false;
		}
		public static bool IsOutOfBounce(int number)
		{
			if (number < 0) { return true; }
			if (number >= 8) { return true; }

			return false;
		}
		#endregion


		#region Operator Overload ==, !=

		

		public override string ToString() => $"({X} | {Y})";

		#endregion
	}
}
