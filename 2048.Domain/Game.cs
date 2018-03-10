using System;
using System.Collections;
using Functional.Maybe;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public enum GameState
	{
		Lost,
		Playing
	}

	public class Game : IEnumerable<CellValue>
	{
		private readonly IPlaceNewCell _newCellPlacer;
		private readonly CellValue[,] _cells = new CellValue[4,4];
		private readonly MoveEvaluator _moveEvaluator;
		
		public Game(IPlaceNewCell newCellPlacer)
		{
			_newCellPlacer = newCellPlacer ?? throw new ArgumentNullException(nameof(newCellPlacer));
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

		public GameState State = GameState.Playing;

		public IEnumerator<CellValue> GetEnumerator() 
			=> _cells.Cast<CellValue>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() 
			=> GetEnumerator();

		private static bool IsInBounds(Position pos)
			=> pos.Row >= 0 && pos.Row <= 3 && pos.Column >= 0 && pos.Column <= 3;

		public void Move(Direction direction)
		{
			var positions = GetPositionSequenceForMove(direction);

			var moves = positions.Select(position => GetMove(position, direction));

			var anyMoves = false;
			
			foreach (var (number, origin, target) in moves)
			{
				target.Match(
					targetPos =>
					{
						MoveToTarget(origin, targetPos, number);
						anyMoves = true;
					},
					() => { }
				);
			}
			
			if (anyMoves)
			{
				PlaceNewCell();

				State = EvaluateGameState();
			}
		}

		private void PlaceNewCell()
		{
			var emptyPositions = EmptyPositions.ToArray();

			var newCellPos = emptyPositions[_newCellPlacer.ChoosePositionIndex(emptyPositions.Length - 1)];

			this[newCellPos] = _newCellPlacer.ChooseValue();
		}

		private GameState EvaluateGameState() 
			=> IsGameLost ? GameState.Lost : GameState.Playing;

		private bool IsGameLost 
			=> AllPositions.All(position => GetNeighbors(position).All(neighbor => !this[position].Equals(this[neighbor])));

		private (int number, Position origin, Maybe<Position> target) GetMove(Position origin, Direction direction)
			=> this[origin].Match(
				number => (number, origin, _moveEvaluator.FindMoveTarget(origin, direction)),
				() => (0, origin, Maybe<Position>.Nothing)
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
		
		private IEnumerable<Position> EmptyPositions
			=> AllPositions.Where(position => !this[position].HasValue);

		private IEnumerable<Position> AllPositions
			=> Enumerable.Range(0, 4)
				.SelectMany(row => Enumerable.Range(0, 4).Select(column => new Position(row, column)));

		private IEnumerable<Position> GetNeighbors(Position position)
		{
			var offsets = new [] { new Position(-1, 0), new Position(1, 0), new Position(0, -1), new Position(0, 1) };

			return offsets.Select(offset => position + offset).Where(neighbor => IsInBounds(neighbor));
		}
	}
}
