using System.Collections.Generic;

namespace _2048
{
	public class Game
	{
		private readonly Board _board;
		private readonly NewCellPlacer _newCellPlacer;
		private readonly CellMover _cellMover;
		private readonly GameStateEvaluator _stateEvaluator;
		
		public Game(IChooseNewCell chooseNewCell)
		{
			_board = new Board();

			_newCellPlacer = new NewCellPlacer(chooseNewCell, _board);
			_cellMover = new CellMover(_board);
			_stateEvaluator = new GameStateEvaluator(_board);
		}

		public GameState State { get; private set; }

		public CellValue this[int row, int column]
		{
			get => _board[new Position(row, column)];
			set => _board[new Position(row, column)] = value;
		}

		public IEnumerable<CellValue> Values => _board;
		
		public void Move(Direction direction)
		{
			if (_cellMover.MoveCells(direction))
			{
				_newCellPlacer.PlaceNewCell();

				State = _stateEvaluator.EvaluateGameState();
			}
		}
	}
}
