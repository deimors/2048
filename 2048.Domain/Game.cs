using Functional.Maybe;
using System;
using System.Linq;

namespace _2048
{
	public class Game
	{
		private readonly Maybe<int>[,] _cells = new Maybe<int>[4,4];

		public Maybe<int> this[int row, int column]
		{
			get => _cells[row, column];
			set => _cells[row, column] = value;
		}

		public void Move(Direction direction)
		{
			switch (direction)
			{
				case Direction.Right:
					foreach (var row in Enumerable.Range(0, 4))
					{
						foreach (var column in Enumerable.Range(0, 4).Reverse())
						{
							this[row, column].Match(
								number =>
								{
									this[row, column] = Maybe<int>.Nothing;

									var moveColumn = Enumerable.Range(column + 1, 3 - column)
										.FirstMaybe(scanColumn => this[row, scanColumn].HasValue)
										.SelectOrElse(
											neighborColumn => neighborColumn - 1,
											() => 3
										);

									this[row, moveColumn] = number.ToMaybe();
								},
								() => { }
							);
						}
					}
					break;

				case Direction.Down:
					foreach (var row in Enumerable.Range(0, 4).Reverse())
					{
						foreach (var column in Enumerable.Range(0, 4))
						{
							this[row, column].Match(
								number =>
								{
									this[row, column] = Maybe<int>.Nothing;

									var moveRow = Enumerable.Range(row + 1, 3 - row)
										.FirstMaybe(scanRow => this[scanRow, column].HasValue)
										.SelectOrElse(
											neighborRow => neighborRow - 1,
											() => 3
										);

									this[moveRow, column] = number.ToMaybe();
								},
								() => { }
							);
						}
					}
					break;

				case Direction.Left:
					foreach (var row in Enumerable.Range(0, 4))
					{
						foreach (var column in Enumerable.Range(0, 4))
						{
							this[row, column].Match(
								number =>
								{
									this[row, column] = Maybe<int>.Nothing;

									var moveColumn = Enumerable.Range(0, column).Reverse()
										.FirstMaybe(scanColumn => this[row, scanColumn].HasValue)
										.SelectOrElse(
											neighborColumn => neighborColumn + 1,
											() => 0
										);

									this[row, moveColumn] = number.ToMaybe();
								},
								() => { }
							);
						}
					}
					break;

				case Direction.Up:
					foreach (var row in Enumerable.Range(0, 4))
					{
						foreach (var column in Enumerable.Range(0, 4))
						{
							this[row, column].Match(
								number =>
								{
									this[row, column] = Maybe<int>.Nothing;

									var moveRow = Enumerable.Range(0, row).Reverse()
										.FirstMaybe(scanRow => this[scanRow, column].HasValue)
										.SelectOrElse(
											neighborRow => neighborRow + 1,
											() => 0
										);

									this[moveRow, column] = number.ToMaybe();
								},
								() => { }
							);
						}
					}
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
			}
		}
	}
}
