namespace _2048
{
	internal static class BoardExtensions
	{
		public static bool IsInBounds(this IBoard board, Position pos)
			=> pos.Row >= 0 && pos.Row < board.Height && pos.Column >= 0 && pos.Column < board.Width;
	}
}