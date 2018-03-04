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
		private readonly GetCellValue _getCellValue;
		private readonly IsInBounds _isInBounds;

		public MoveEvaluator(GetCellValue getCellValue, IsInBounds isInBounds)
		{
			_getCellValue = getCellValue ?? throw new ArgumentNullException(nameof(getCellValue));
			_isInBounds = isInBounds ?? throw new ArgumentNullException(nameof(isInBounds));
		}
		
		public Maybe<Position> FindMoveTarget(Position position, Direction direction)
			=> _getCellValue(position).Match(
				number => FindMoveTarget(number, Project(position, direction).ToArray()),
				() => Maybe<Position>.Nothing
			);

		private Maybe<Position> FindMoveTarget(int startNumber, (Maybe<Position> previous, Position current)[] projection) 
			=> projection
				.FirstMaybe(pair => _getCellValue(pair.current).HasValue)
				.SelectOrElse(
					somePair => _getCellValue(somePair.current).Value == startNumber
						? somePair.current.ToMaybe()
						: somePair.previous,
					() => projection.LastMaybe().Select(pair => pair.current)
				);

		private IEnumerable<(Maybe<Position> previous, Position current)> Project(Position start, Direction direction)
		{
			var increment = GetIncrement(direction);
			var current = start + increment;
			var previous = Maybe<Position>.Nothing;

			while (_isInBounds(current))
			{
				yield return (previous, current);
				previous = current.ToMaybe();
				current += increment;
			}
		}
		
		private static Position GetIncrement(Direction direction)
		{
			switch (direction)
			{
				case Direction.Right: return new Position(0, 1);
				case Direction.Down: return new Position(1, 0);
				case Direction.Left: return new Position(0, -1);
				case Direction.Up: return new Position(-1, 0);
				default: throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Unknown Direction: {direction}");
			}
		}
	}
}