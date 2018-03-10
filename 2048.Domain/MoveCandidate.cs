using Functional.Maybe;

namespace _2048
{
	internal class MoveCandidate
	{
		public int Number { get; }
		public Position Origin { get; }
		public Maybe<Position> Target { get; }

		public MoveCandidate(int number, Position origin, Maybe<Position> target)
		{
			Number = number;
			Origin = origin;
			Target = target;
		}
	}
}