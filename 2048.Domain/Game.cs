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
			foreach (var row in Enumerable.Range(0, 4))
			{
				foreach (var column in Enumerable.Range(0, 4))
				{
					var cellValue = this[row, column];

					cellValue.Match(
						number =>
						{
							this[row, column] = Maybe<int>.Nothing;

							switch (direction)
							{
								case Direction.Right:
									this[row, 3] = number.ToMaybe();
									break;
								case Direction.Down:
									this[3, column] = number.ToMaybe();
									break;
								case Direction.Left:
									this[row, 0] = number.ToMaybe();
									break;
								case Direction.Up:
									this[0, column] = number.ToMaybe();
									break;
								default:
									throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
							}
						},
						() => { }
					);
				}
			}
		}
	}
}
