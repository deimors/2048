using System;
using System.Linq;

namespace _2048
{
	internal class GameStateEvaluator
	{
		private readonly Board _board;

		public GameStateEvaluator(Board board)
		{
			_board = board ?? throw new ArgumentNullException(nameof(board));
		}

		public GameState EvaluateGameState()
			=> IsGameLost
				? GameState.Lost
				: IsGameWon
					? GameState.Won
					: GameState.Playing;

		private bool IsGameLost
			=> _board.AllPositions.All(AllNeighborsAreDifferent);

		private bool AllNeighborsAreDifferent(Position position)
			=> position.Neighbors
				.Where(neighbor => _board.IsInBounds(neighbor))
				.All(neighbor => !_board[position].Equals(_board[neighbor]));

		private bool IsGameWon
			=> _board.Any(value => value.Equals(2048));
	}
}