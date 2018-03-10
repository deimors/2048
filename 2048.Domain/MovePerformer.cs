using System;
using System.Collections.Generic;
using System.Linq;
using Functional.Maybe;

namespace _2048
{
	internal class MovePerformer
	{
		private readonly Board _board;

		private readonly MoveEvaluator _moveEvaluator;

		public MovePerformer(Board board)
		{
			_board = board ?? throw new ArgumentNullException(nameof(board));
			_moveEvaluator = new MoveEvaluator(board);
		}

		public bool MoveCells(Direction direction)
		{
			var positions = GetPositionSequenceForMove(direction);

			var candidates = positions.Select(position => GetMoveCandidate(position, direction));

			var anyMoves = false;

			foreach (var candidate in candidates)
			{
				candidate.Target.Match(
					targetPos =>
					{
						MoveToTarget(candidate.Origin, targetPos, candidate.Number);
						anyMoves = true;
					},
					() => { }
				);
			}

			return anyMoves;
		}

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
				number => new MoveCandidate(number, origin, _moveEvaluator.FindMoveTarget(origin, direction)),
				() => new MoveCandidate(0, origin, Maybe<Position>.Nothing)
			);

		private void MoveToTarget(Position origin, Position target, int originNumber)
		{
			_board[origin] = CellValue.Empty;

			_board[target] = _board[target].Match(
				targetNumber => originNumber + targetNumber,
				() => originNumber
			);
		}
	}
}