using System;
using System.Collections.Generic;
using System.Linq;
using Functional.Maybe;

namespace _2048
{
	internal delegate CellValue GetCellValue(Position position);

	internal delegate bool IsInBounds(Position position);

	internal class MoveEvaluator
	{
		private readonly Board _board;
		
		public MoveEvaluator(Board board)
		{
			_board = board ?? throw new ArgumentNullException(nameof(board));
		}

		public IEnumerable<MoveCandidate> GetMoveCandidates(Direction direction)
			=> GetPositionSequenceForMove(direction)
				.Select(position => GetMoveCandidate(position, direction));

		private IEnumerable<Position> GetPositionSequenceForMove(Direction direction)
		{
			var rows = Enumerable.Range(0, _board.Height);
			var columns = Enumerable.Range(0, _board.Width);

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

		private MoveCandidate GetMoveCandidate(Position origin, Direction direction)
			=> _board[origin].Match(
				number => new MoveCandidate(number, origin, _board.FindMoveTarget(origin, direction)),
				() => new MoveCandidate(0, origin, Maybe<Position>.Nothing)
			);
	}
}