using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public struct Position
	{
		private static readonly Position[] NeighborOffsets =
		{
			new Position(-1, 0),
			new Position(1, 0),
			new Position(0, -1),
			new Position(0, 1)
		};

		public int Row { get; }
		public int Column { get; }

		public Position(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public IEnumerable<Position> Neighbors
		{
			get
			{
				var position = this;
				return NeighborOffsets.Select(offset => position + offset);
			}
		}

		public override string ToString()
			=> $"({Row}, {Column})";

		public static Position operator +(Position first, Position second)
			=> new Position(first.Row + second.Row, first.Column + second.Column);
	}
}