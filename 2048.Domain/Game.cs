using System.Collections;
using System.Collections.Generic;

namespace _2048
{
	public class Game : IEnumerable<CellValue>
	{
		private readonly Board _board;
		private readonly NewCellPlacer _newCellPlacer;
		private readonly MovePerformer _movePerformer;
		private readonly GameStateEvaluator _stateEvaluator;
		
		public Game(INewCellChooser newCellChooser)
		{
			_board = new Board();

			_newCellPlacer = new NewCellPlacer(newCellChooser, _board);
			_movePerformer = new MovePerformer(_board);
			_stateEvaluator = new GameStateEvaluator(_board);
		}

		public GameState State { get; private set; }

		public CellValue this[int row, int column]
		{
			get => _board[new Position(row, column)];
			set => _board[new Position(row, column)] = value;
		}

		public IEnumerator<CellValue> GetEnumerator() => _board.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		
		public void Move(Direction direction)
		{
			if (_movePerformer.MoveCells(direction))
			{
				_newCellPlacer.PlaceNewCell();

				State = _stateEvaluator.EvaluateGameState();
			}
		}
	}
}
