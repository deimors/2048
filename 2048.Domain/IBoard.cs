using System.Collections.Generic;

namespace _2048
{
	internal interface IBoard : IEnumerable<CellValue>
	{
		int Height { get; }
		int Width { get; }

		CellValue this[Position position] { get; set; }

		IEnumerable<Position> AllPositions { get; }
	}
}