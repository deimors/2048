using FluentAssertions;
using System.Linq;
using Xunit;

namespace _2048.Domain.Tests
{
	public class GameTests
	{
		[Fact]
		public void WhenEmpty_AllCellsContainNothing()
		{
			var sut = new Game();

			foreach (var row in Enumerable.Range(0, 4))
			{
				foreach (var column in Enumerable.Range(0, 4))
				{
					sut[row, column].Should().Be(CellValue.Empty);
				}
			}
		}
	}
}
