using Functional.Maybe;
using System;

namespace _2048
{
	internal class CellMover
	{
		private readonly Board _board;

		private readonly MoveEvaluator _moveEvaluator;

		public CellMover(Board board)
		{
			_board = board ?? throw new ArgumentNullException(nameof(board));
			_moveEvaluator = new MoveEvaluator(board);
		}

		public bool MoveAllCells(Direction direction)
		{
			var anyMoves = false;

			foreach (var candidate in _moveEvaluator.GetMoveCandidates(direction))
			{
				candidate.Target.Match(
					targetPos =>
					{
						MoveToTarget(candidate.Origin, targetPos, candidate.Number);
						anyMoves = true;
					},
					() => { }
				);
			}

			return anyMoves;
		}
		
		private void MoveToTarget(Position origin, Position target, int originNumber)
		{
			_board[origin] = CellValue.Empty;

			_board[target] = _board[target].Match(
				targetNumber => originNumber + targetNumber,
				() => originNumber
			);
		}
	}
}