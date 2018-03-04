using System.Linq;
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
		public void When2Along00To02Line_MoveRight_2At02And4At03()
		{
			var sut = new Game
			{
				[0, 0] = 2,
				[0, 1] = 2,
				[0, 2] = 2
			};

			sut.Move(Direction.Right);

			sut[0, 2].Should().Be(2);
			sut[0, 3].Should().Be(4);
		}

		[Fact]
		public void When2At00And4At02AndAt03_MoveRight_2At02And8At03()
		{
			var sut = new Game
			{
				[0, 0] = 2,
				[0, 2] = 4,
				[0, 3] = 4
			};

			sut.Move(Direction.Right);

			sut[0, 2].Should().Be(2);
			sut[0, 3].Should().Be(8);
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

		[Fact]
		public void When2At02AndAt03_MoveLeft_4At00()
		{
			var sut = new Game
			{
				[0, 2] = 2,
				[0, 3] = 2
			};

			sut.Move(Direction.Left);

			sut[0, 0].Should().Be(4);
		}

		[Fact]
		public void When2At00AndAt10_MoveDown_4At30()
		{
			var sut = new Game
			{
				[0, 0] = 2,
				[1, 0] = 2
			};

			sut.Move(Direction.Down);

			sut[3, 0].Should().Be(4);
		}

		[Fact]
		public void When2At20AndAt30_MoveUp_4At00()
		{
			var sut = new Game
			{
				[2, 0] = 2,
				[3, 0] = 2
			};

			sut.Move(Direction.Up);

			sut[0, 0].Should().Be(4);
		}
	}
}