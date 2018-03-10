using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
	internal class NewCellPlacer
	{
		private readonly IChooseNewCell _chooseNewCell;
		private readonly Board _board;

		public NewCellPlacer(IChooseNewCell chooseNewCell, Board board)
		{
			_chooseNewCell = chooseNewCell ?? throw new ArgumentNullException(nameof(chooseNewCell));
			_board = board ?? throw new ArgumentNullException(nameof(board));
		}

		public void PlaceNewCell()
		{
			var emptyPositions = EmptyPositions.ToArray();

			var newCellPos = emptyPositions[_chooseNewCell.ChoosePositionIndex(emptyPositions.Length - 1)];

			_board[newCellPos] = _chooseNewCell.ChooseValue();
		}

		private IEnumerable<Position> EmptyPositions
			=> _board.AllPositions.Where(position => _board[position].IsEmpty);
	}
}