using FluentAssertions;
using Xunit;

namespace _2048.Domain.Tests
{
	public class GameOverTests
	{
		[Fact]
		public void WhenCheckerboard2And4ExceptLastCellAndNewCellIs4_MoveRight_GameStateIsLost()
		{
			var sut = new Game(new FakeNewCellChooser(0, 4))
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
			var sut = new Game(new FakeNewCellChooser(0, 2))
			{
				[0, 0] = 2,  [0, 1] = 4, [0, 2] = 2, [0, 3] = 4,
				[1, 0] = 4,  [1, 1] = 2, [1, 2] = 4, [1, 3] = 2,
				[2, 0] = 2,  [2, 1] = 4, [2, 2] = 2, [2, 3] = 4,
				[3, 0] = 2,  [3, 1] = 4, [3, 2] = 2
			};

			sut.Move(Direction.Right);

			sut.State.Should().Be(GameState.Playing);
		}

		[Fact]
		public void When1024At00And01_MoveRight_GameStateIsWon()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2))
			{
				[0, 0] = 1024,
				[0, 1] = 1024
			};

			sut.Move(Direction.Right);

			sut.State.Should().Be(GameState.Won);
		}

		[Fact]
		public void When1024At00And01_MoveLeft_GameStateIsWon()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2))
			{
				[0, 0] = 1024,
				[0, 1] = 1024
			};

			sut.Move(Direction.Left);

			sut.State.Should().Be(GameState.Won);
		}

		[Fact]
		public void When1024At00And01_MoveUp_GameStateIsPlaying()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2))
			{
				[0, 0] = 1024,
				[0, 1] = 1024
			};

			sut.Move(Direction.Up);

			sut.State.Should().Be(GameState.Playing);
		}

		[Fact]
		public void When1024At00And01_MoveDown_GameStateIsPlaying()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2))
			{
				[0, 0] = 1024,
				[0, 1] = 1024
			};

			sut.Move(Direction.Down);

			sut.State.Should().Be(GameState.Playing);
		}
	}
}