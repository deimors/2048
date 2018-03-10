using Functional.Maybe;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	internal static class MoveTargetEvaluation
	{
		private static readonly IReadOnlyDictionary<Direction, Position> MoveIncrements = new Dictionary<Direction, Position>
		{
			{Direction.Right, new Position(0, 1)},
			{Direction.Down, new Position(1, 0)},
			{Direction.Left, new Position(0, -1)},
			{Direction.Up, new Position(-1, 0)}
		};
		
		public static Maybe<Position> FindMoveTarget(this Board board, Position position, Direction direction)
			=> board[position].Match(
				number => board.FindMoveTarget(number, board.BuildPositionPairs(position, direction).ToArray()),
				() => Maybe<Position>.Nothing
			);
		
		private static Maybe<Position> FindMoveTarget(this Board board, int startNumber, PositionPair[] projection) 
			=> projection
				.FirstMaybe(pair => !board[pair.Current].IsEmpty)
				.SelectOrElse(
					somePair => board[somePair.Current].Equals(startNumber)
						? somePair.Current.ToMaybe()
						: somePair.Previous,
					() => projection.LastMaybe().Select(pair => pair.Current)
				);

		private static IEnumerable<PositionPair> BuildPositionPairs(this Board board, Position start, Direction direction)
		{
			var increment = GetIncrement(direction);
			var current = start + increment;
			var previous = Maybe<Position>.Nothing;

			while (board.IsInBounds(current))
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