namespace _2048
{
	internal class Position
	{
		public int Row { get; }
		public int Column { get; }

		public Position(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public override string ToString()
			=> $"({Row}, {Column})";

		public static Position operator +(Position first, Position second)
			=> new Position(first.Row + second.Row, first.Column + second.Column);
	}
}