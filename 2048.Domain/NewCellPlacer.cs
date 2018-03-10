using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	internal class NewCellPlacer
	{
		private readonly INewCellChooser _newCellChooser;
		private readonly IBoard _board;

		public NewCellPlacer(INewCellChooser newCellChooser, IBoard board)
		{
			_newCellChooser = newCellChooser ?? throw new ArgumentNullException(nameof(newCellChooser));
			_board = board ?? throw new ArgumentNullException(nameof(board));
		}

		public void PlaceNewCell()
		{
			var emptyPositions = EmptyPositions.ToArray();

			var newCellPos = emptyPositions[_newCellChooser.ChoosePositionIndex(emptyPositions.Length - 1)];

			_board[newCellPos] = _newCellChooser.ChooseValue();
		}

		private IEnumerable<Position> EmptyPositions
			=> _board.AllPositions.Where(position => !_board[position].HasValue);
	}
}