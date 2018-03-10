using Functional.Maybe;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	internal static class MoveCandidateEvaluation
	{
		public static IEnumerable<MoveCandidate> GetMoveCandidates(this Board board, Direction direction)
			=> board.GetPositionSequenceForMove(direction)
				.Select(position => board.GetMoveCandidate(position, direction));

		private static IEnumerable<Position> GetPositionSequenceForMove(this Board board, Direction direction)
		{
			var rows = Enumerable.Range(0, board.Height);
			var columns = Enumerable.Range(0, board.Width);

			switch (direction)
			{
				case Direction.Down:
					rows = rows.Reverse();
					break;
				case Direction.Right:
					columns = columns.Reverse();
					break;
			}

			return rows.SelectMany(row => columns.Select(column => new Position(row, column)));
		}

		private static MoveCandidate GetMoveCandidate(this Board board, Position origin, Direction direction)
			=> board[origin].Match(
				number => new MoveCandidate(number, origin, board.FindMoveTarget(origin, direction)),
				() => new MoveCandidate(0, origin, Maybe<Position>.Nothing)
			);
	}
}