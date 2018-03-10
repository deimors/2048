using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public class Game : IEnumerable<CellValue>, IBoard
	{
		private readonly IPlaceNewCell _newCellPlacer;
		private readonly CellValue[,] _cells = new CellValue[4,4];
		private readonly MovePerformer _movePerformer;
		
		public Game(IPlaceNewCell newCellPlacer)
		{
			_newCellPlacer = newCellPlacer ?? throw new ArgumentNullException(nameof(newCellPlacer));
			_movePerformer = new MovePerformer(this);
		}

		public CellValue this[int row, int column]
		{
			get => _cells[row, column];
			set => _cells[row, column] = value;
		}

		public CellValue this[Position pos]
		{
			get => _cells[pos.Row, pos.Column];
			set => _cells[pos.Row, pos.Column] = value;
		}

		public GameState State { get; private set; }

		public IEnumerator<CellValue> GetEnumerator() 
			=> _cells.Cast<CellValue>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() 
			=> GetEnumerator();

		private bool IsInBounds(Position pos)
			=> pos.Row >= 0 && pos.Row < Height && pos.Column >= 0 && pos.Column < Width;

		public int Height => _cells.GetLength(0);
		public int Width => _cells.GetLength(1);
		
		public void Move(Direction direction)
		{
			if (_movePerformer.MoveCells(direction))
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
			=> IsGameLost 
				? GameState.Lost 
				: IsGameWon 
					? GameState.Won 
					: GameState.Playing;

		private bool IsGameLost 
			=> AllPositions.All(AllNeighborsAreDifferent);

		private bool AllNeighborsAreDifferent(Position position) 
			=> position.Neighbors
				.Where(neighbor => IsInBounds(neighbor))
				.All(neighbor => !this[position].Equals(this[neighbor]));

		private bool IsGameWon
			=> this.Any(value => value.Equals(2048));
		
		private IEnumerable<Position> EmptyPositions
			=> AllPositions.Where(position => !this[position].HasValue);

		private IEnumerable<Position> AllPositions
			=> Enumerable.Range(0, Height)
				.SelectMany(row => Enumerable.Range(0, Width).Select(column => new Position(row, column)));
	}
}
