using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public class Board : IEnumerable<CellValue>
	{
		private readonly CellValue[,] _cells = new CellValue[4, 4];

		public int Height => _cells.GetLength(0);
		public int Width => _cells.GetLength(1);

		public CellValue this[Position pos]
		{
			get => _cells[pos.Row, pos.Column];
			set => _cells[pos.Row, pos.Column] = value;
		}

		public IEnumerator<CellValue> GetEnumerator()
			=> _cells.Cast<CellValue>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
		
		public IEnumerable<Position> AllPositions
			=> Enumerable.Range(0, Height)
				.SelectMany(row => Enumerable.Range(0, Width).Select(column => new Position(row, column)));
	}
}