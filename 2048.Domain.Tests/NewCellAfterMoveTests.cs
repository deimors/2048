using FluentAssertions;
using Xunit;

namespace _2048.Domain.Tests
{
	public class NewCellAfterMoveTests
	{
		[Fact]
		public void When8At00_MoveRight_Some2InGame()
		{
			var sut = new Game(new FakeChooseNewCell(0, 2))
			{
				[0, 0] = 8
			};

			sut.Move(Direction.Right);

			sut.Values.Should().ContainSingle(cellValue => cellValue.Equals(2));
		}

		[Fact]
		public void When8And16And32And64Along00To03Line_MoveRight_NotSome2InGame()
		{
			var sut = new Game(new FakeChooseNewCell(0, 4))
			{
				[0, 0] = 8,
				[0, 1] = 16,
				[0, 2] = 32,
				[0, 3] = 64
			};

			sut.Move(Direction.Right);

			sut.Values.Should().NotContain(cellValue => cellValue.Equals(4));
		}
	}
}