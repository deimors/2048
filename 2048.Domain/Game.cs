using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	internal class Position
	{
		public int Row { get; }
		public int Column { get; }

		public Position(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public override string ToString()
			=> $"({Row}, {Column})";

		public static Position operator +(Position first, Position second)
			=> new Position(first.Row + second.Row, first.Column + second.Column);
	}

	public class Game
	{
		private readonly Maybe<int>[,] _cells = new Maybe<int>[4,4];

		public Maybe<int> this[int row, int column]
		{
			get => _cells[row, column];
			set => _cells[row, column] = value;
		}

		private Maybe<int> this[Position pos]
		{
			get => _cells[pos.Row, pos.Column];
			set => _cells[pos.Row, pos.Column] = value;
		}
		
		public void Move(Direction direction)
		{
			var positions = GetPositionsForMove(direction);

			foreach (var position in positions)
			{
				this[position.Row, position.Column].Match(
					number => FindMovePosition(number, Project(position, direction))
						.Match(
							newPosition =>
							{
								this[position] = Maybe<int>.Nothing;

								this[newPosition] = this[newPosition].SelectOrElse(
									prevValue => number + prevValue,
									() => number
								).ToMaybe();
							},
							() => { }
						),
					() => { }
				);
			}
		}

		private static IEnumerable<Position> GetPositionsForMove(Direction moveDirection)
		{
			var rows = Enumerable.Range(0, 4);
			var columns = Enumerable.Range(0, 4);

			switch (moveDirection)
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

		private static bool IsInBounds(Position pos)
			=> pos.Row >= 0 && pos.Row <= 3 && pos.Column >= 0 && pos.Column <= 3;

		private Maybe<Position> FindMovePosition(int startNumber, IEnumerable<(Maybe<Position> previous, Position current)> projection) 
			=> projection
				.FirstMaybe(pair => this[pair.current.Row, pair.current.Column].HasValue)
				.SelectOrElse(
					somePair => this[somePair.current.Row, somePair.current.Column].Value == startNumber
						? somePair.current.ToMaybe()
						: somePair.previous,
					() => projection.LastMaybe().Select(pair => pair.current)
				);
	}

	internal static class EnumerableExtensions
	{
		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T prependItem)
		{
			yield return prependItem;

			foreach (var item in source)
				yield return item;
		}

		public static IEnumerable<(T first, T second)> Pairwise<T>(this IEnumerable<T> source)
		{
			using (var enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
					yield break;

				var first = enumerator.Current;

				while (enumerator.MoveNext())
				{
					var second = enumerator.Current;
					yield return (first, second);
					first = second;
				}
			}
		}
	}
}
