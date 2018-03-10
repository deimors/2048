namespace _2048
{
	internal interface IBoard
	{
		int Height { get; }
		int Width { get; }

		CellValue this[Position position] { get; set; }
	}
}