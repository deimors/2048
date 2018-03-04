using Functional.Maybe;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public class Game
	{
		private readonly CellValue[,] _cells = new CellValue[4,4];
		private readonly MoveEvaluator _moveEvaluator;

		public Game()
		{
			_moveEvaluator = new MoveEvaluator(pos => this[pos], IsInBounds);
		}

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

		private static bool IsInBounds(Position pos)
			=> pos.Row >= 0 && pos.Row <= 3 && pos.Column >= 0 && pos.Column <= 3;

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
				originNumber => _moveEvaluator.FindMoveTarget(origin, direction)
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
	}
}
