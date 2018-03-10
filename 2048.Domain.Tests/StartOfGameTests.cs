using FluentAssertions;
using System.Linq;
using Xunit;

namespace _2048.Domain.Tests
{
	public class StartOfGameTests
	{
		[Fact]
		public void WhenEmpty_AllCellsContainNothing()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2));

			foreach (var row in Enumerable.Range(0, 4))
			{
				foreach (var column in Enumerable.Range(0, 4))
				{
					sut[row, column].Should().Be(CellValue.Empty);
				}
			}
		}

		[Fact]
		public void WhenEmpty_GameStateIsPlaying()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2));

			sut.State.Should().Be(GameState.Playing);
		}

		[Fact]
		public void When2At00_GameContains2()
		{
			var sut = new Game(new FakeNewCellChooser(0, 2))
			{
				[0, 0] = 2
			};
			
			sut.Should().Contain(2);
		}
	}
}
