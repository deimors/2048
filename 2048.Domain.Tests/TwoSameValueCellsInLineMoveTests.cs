using FluentAssertions;
using Xunit;

namespace _2048.Domain.Tests
{
	public class TwoSameValueCellsInLineMoveTests
	{
		[Fact]
		public void When2At00AndAt01_MoveRight_4At03()
		{
			var sut = new Game
			{
				[0, 0] = 2,
				[0, 1] = 2
			};

			sut.Move(Direction.Right);

			sut[0, 3].Should().Be(4);
		}

		[Fact]
		public void When2At00AndAt01And4At02AndAt03_MoveRight_4At02And8At03()
		{
			var sut = new Game
			{
				[0, 0] = 2,
				[0, 1] = 2,
				[0, 2] = 4,
				[0, 3] = 4
			};

			sut.Move(Direction.Right);

			sut[0, 2].Should().Be(4);
			sut[0, 3].Should().Be(8);
		}
	}
}