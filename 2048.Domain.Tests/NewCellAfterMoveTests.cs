using FluentAssertions;
using Xunit;

namespace _2048.Domain.Tests
{
	public class NewCellAfterMoveTests
	{
		[Fact]
		public void When8At00_MoveRight_Some2InGame()
		{
			var sut = new Game(new FakePlaceNewCell(0, 2))
			{
				[0, 0] = 8
			};

			sut.Move(Direction.Right);

			sut.Should().ContainSingle(cellValue => cellValue.Equals(2));
		}

		[Fact]
		public void When8And16And32And64Along00To03Line_MoveRight_NotSome2InGame()
		{
			var sut = new Game(new FakePlaceNewCell(0, 4))
			{
				[0, 0] = 8,
				[0, 1] = 16,
				[0, 2] = 32,
				[0, 3] = 64
			};

			sut.Move(Direction.Right);

			sut.Should().NotContain(cellValue => cellValue.Equals(4));
		}
	}

	public class GameOverTests
	{
		[Fact]
		public void WhenCheckerboard2And4ExceptLastCellAndNewCellIs4_MoveRight_GameStateIsLost()
		{
			var sut = new Game(new FakePlaceNewCell(0, 4))
			{
				[0, 0] = 2,  [0, 1] = 4, [0, 2] = 2, [0, 3] = 4,
				[1, 0] = 4,  [1, 1] = 2, [1, 2] = 4, [1, 3] = 2,
				[2, 0] = 2,  [2, 1] = 4, [2, 2] = 2, [2, 3] = 4,
				[3, 0] = 2,  [3, 1] = 4, [3, 2] = 2
			};

			sut.Move(Direction.Right);

			sut.State.Should().Be(GameState.Lost);
		}

		[Fact]
		public void WhenCheckerboard2And4ExceptLastCellAndNewCellIs2_MoveRight_GameStateIsPlaying()
		{
			var sut = new Game(new FakePlaceNewCell(0, 2))
			{
				[0, 0] = 2,  [0, 1] = 4, [0, 2] = 2, [0, 3] = 4,
				[1, 0] = 4,  [1, 1] = 2, [1, 2] = 4, [1, 3] = 2,
				[2, 0] = 2,  [2, 1] = 4, [2, 2] = 2, [2, 3] = 4,
				[3, 0] = 2,  [3, 1] = 4, [3, 2] = 2
			};

			sut.Move(Direction.Right);

			sut.State.Should().Be(GameState.Playing);
		}
	}
}