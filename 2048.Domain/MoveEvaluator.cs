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

		private static readonly IReadOnlyDictionary<Direction, Position> MoveIncrements = new Dictionary<Direction, Position>
		{
			{ Direction.Right, new Position(0, 1) },
			{ Direction.Down, new Position(1, 0) },
			{ Direction.Left, new Position(0, -1)},
			{ Direction.Up, new Position(-1, 0)}
		};
		
		public MoveEvaluator(Board board)
		{
			_board = board ?? throw new ArgumentNullException(nameof(board));
		}
		
		public Maybe<Position> FindMoveTarget(Position position, Direction direction)
			=> _board[position].Match(
				number => FindMoveTarget(number, Project(position, direction).ToArray()),
				() => Maybe<Position>.Nothing
			);

		private Maybe<Position> FindMoveTarget(int startNumber, PositionPair[] projection) 
			=> projection
				.FirstMaybe(pair => !_board[pair.Current].IsEmpty)
				.SelectOrElse(
					somePair => _board[somePair.Current].Equals(startNumber)
						? somePair.Current.ToMaybe()
						: somePair.Previous,
					() => projection.LastMaybe().Select(pair => pair.Current)
				);

		private IEnumerable<PositionPair> Project(Position start, Direction direction)
		{
			var increment = GetIncrement(direction);
			var current = start + increment;
			var previous = Maybe<Position>.Nothing;

			while (_board.IsInBounds(current))
			{
				yield return new PositionPair(previous, current);
				previous = current.ToMaybe();
				current += increment;
			}
		}

		private static Position GetIncrement(Direction direction)
			=> MoveIncrements[direction];

		private struct PositionPair
		{
			public readonly Maybe<Position> Previous;
			public readonly Position Current;

			public PositionPair(Maybe<Position> previous, Position current)
			{
				Previous = previous;
				Current = current;
			}
		}
	}
}