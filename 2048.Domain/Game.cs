using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public class Game
	{
		private readonly CellValue[,] _cells = new CellValue[4,4];

		public CellValue this[int row, int column]
		{
			get => _cells[row, column];
			set => _cells[row, column] = value;
		}

		private CellValue this[Position pos]
		{
			get => _cells[pos.Row, pos.Column];
			set => _cells[pos.Row, pos.Column] = value;
		}
		
		public void Move(Direction direction)
		{
			var positions = GetPositionSequenceForMove(direction);

			foreach (var position in positions)
			{
				MoveNumberInDirection(position, direction);
			}
		}

		private void MoveNumberInDirection(Position origin, Direction direction) 
			=> this[origin].Apply(
				originNumber => FindMoveTarget(origin, direction)
					.Match(
						target => MoveToTarget(origin, target, originNumber),
						() => { }
					),
				() => { }
			);

		private void MoveToTarget(Position origin, Position target, int originNumber)
		{
			this[origin] = CellValue.Empty;
			
			this[target] = this[target].Match(
				targetNumber => originNumber + targetNumber,
				() => originNumber
			);
		}

		private static IEnumerable<Position> GetPositionSequenceForMove(Direction direction)
		{
			var rows = Enumerable.Range(0, 4);
			var columns = Enumerable.Range(0, 4);

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

		private Maybe<Position> FindMoveTarget(Position position, Direction direction)
			=> this[position].Match(
				number => FindMoveTarget(number, Project(position, direction).ToArray()),
				() => Maybe<Position>.Nothing
			);
		
		private Maybe<Position> FindMoveTarget(int startNumber, (Maybe<Position> previous, Position current)[] projection) 
			=> projection
				.FirstMaybe(pair => this[pair.current].HasValue)
				.SelectOrElse(
					somePair => this[somePair.current].Value == startNumber
						? somePair.current.ToMaybe()
						: somePair.previous,
					() => projection.LastMaybe().Select(pair => pair.current)
				);

		private static IEnumerable<(Maybe<Position> previous, Position current)> Project(Position start, Direction direction)
		{
			var increment = GetIncrement(direction);
			var current = start + increment;
			var previous = Maybe<Position>.Nothing;

			while (IsInBounds(current))
			{
				yield return (previous, current);
				previous = current.ToMaybe();
				current += increment;
			}
		}

		private static bool IsInBounds(Position pos)
			=> pos.Row >= 0 && pos.Row <= 3 && pos.Column >= 0 && pos.Column <= 3;
	}
}
