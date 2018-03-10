using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	public class Game : IBoard
	{
		private readonly NewCellPlacer _newCellPlacer;
		private readonly CellValue[,] _cells = new CellValue[4,4];
		private readonly MovePerformer _movePerformer;
		private readonly GameStateEvaluator _stateEvaluator;
		
		public Game(INewCellChooser newCellChooser)
		{
			_newCellPlacer = new NewCellPlacer(newCellChooser, this);
			_movePerformer = new MovePerformer(this);
			_stateEvaluator = new GameStateEvaluator(this);
			
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

		public int Height => _cells.GetLength(0);
		public int Width => _cells.GetLength(1);
		
		public void Move(Direction direction)
		{
			if (_movePerformer.MoveCells(direction))
			{
				_newCellPlacer.PlaceNewCell();

				State = _stateEvaluator.EvaluateGameState();
			}
		}
		
		public IEnumerable<Position> AllPositions
			=> Enumerable.Range(0, Height)
				.SelectMany(row => Enumerable.Range(0, Width).Select(column => new Position(row, column)));
	}
}
