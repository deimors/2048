﻿using FluentAssertions;
using Functional.Maybe;
using Xunit;

namespace _2048.Domain.Tests
{
	public class SingleCellMoveTests
	{
		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 0)]
		[InlineData(3, 0)]
		[InlineData(0, 2)]
		[InlineData(2, 1)]
		public void When2AtRowColumn_MoveRight_RowColumnIsNothingAnd2AtSameRowColumn3(int row, int column)
		{
			var sut = new Game
			{
				[row, column] = 2.ToMaybe()
			};

			sut.Move(Direction.Right);

			sut[row, column].Should().Be(Maybe<int>.Nothing);
			sut[row, 3].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void When2AtRowAndColumn3_MoveRight_2AtSamePlace(int row)
		{
			var sut = new Game
			{
				[row, 3] = 2.ToMaybe()
			};

			sut.Move(Direction.Right);

			sut[row, 3].Should().Be(2.ToMaybe());
		}

		[Fact]
		public void When2Along00To33Diagonal_MoveRight_2Along03To33Line()
		{
			var sut = new Game
			{
				[0, 0] = 2.ToMaybe(),
				[1, 1] = 2.ToMaybe(),
				[2, 2] = 2.ToMaybe(),
				[3, 3] = 2.ToMaybe()
			};

			sut.Move(Direction.Right);

			sut[0, 3].Should().Be(2.ToMaybe());
			sut[1, 3].Should().Be(2.ToMaybe());
			sut[2, 3].Should().Be(2.ToMaybe());
			sut[3, 3].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 0)]
		[InlineData(2, 0)]
		[InlineData(0, 2)]
		[InlineData(2, 1)]
		public void When2AtRowColumn_MoveDown_RowColumnIsNothingAnd2AtRow3SameColumn(int row, int column)
		{
			var sut = new Game
			{
				[row, column] = 2.ToMaybe()
			};

			sut.Move(Direction.Down);

			sut[row, column].Should().Be(Maybe<int>.Nothing);
			sut[3, column].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void When2AtRow3AndColumn_MoveDown_2AtSamePlace(int column)
		{
			var sut = new Game
			{
				[3, column] = 2.ToMaybe()
			};

			sut.Move(Direction.Down);

			sut[3, column].Should().Be(2.ToMaybe());
		}

		[Fact]
		public void When2Along00To33Diagonal_MoveDown_2Along30To33Line()
		{
			var sut = new Game
			{
				[0, 0] = 2.ToMaybe(),
				[1, 1] = 2.ToMaybe(),
				[2, 2] = 2.ToMaybe(),
				[3, 3] = 2.ToMaybe()
			};

			sut.Move(Direction.Down);

			sut[3, 0].Should().Be(2.ToMaybe());
			sut[3, 1].Should().Be(2.ToMaybe());
			sut[3, 2].Should().Be(2.ToMaybe());
			sut[3, 3].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(0, 1)]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(0, 2)]
		[InlineData(2, 1)]
		public void When2AtRowColumn_MoveLeft_RowColumnIsNothingAnd2AtSameRowColumn0(int row, int column)
		{
			var sut = new Game
			{
				[row, column] = 2.ToMaybe()
			};

			sut.Move(Direction.Left);

			sut[row, column].Should().Be(Maybe<int>.Nothing);
			sut[row, 0].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void When2AtRowAndColumn0_MoveLeft_2AtSamePlace(int row)
		{
			var sut = new Game
			{
				[row, 0] = 2.ToMaybe()
			};

			sut.Move(Direction.Left);

			sut[row, 0].Should().Be(2.ToMaybe());
		}

		[Fact]
		public void When2Along00To33Diagonal_MoveLeft_2Along00To30Line()
		{
			var sut = new Game
			{
				[0, 0] = 2.ToMaybe(),
				[1, 1] = 2.ToMaybe(),
				[2, 2] = 2.ToMaybe(),
				[3, 3] = 2.ToMaybe()
			};

			sut.Move(Direction.Left);

			sut[0, 0].Should().Be(2.ToMaybe());
			sut[1, 0].Should().Be(2.ToMaybe());
			sut[2, 0].Should().Be(2.ToMaybe());
			sut[3, 0].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(1, 2)]
		[InlineData(2, 3)]
		[InlineData(2, 2)]
		[InlineData(3, 1)]
		public void When2AtRowColumn_MoveUp_RowColumnIsNothingAnd2AtRow0SameColumn(int row, int column)
		{
			var sut = new Game
			{
				[row, column] = 2.ToMaybe()
			};

			sut.Move(Direction.Up);

			sut[row, column].Should().Be(Maybe<int>.Nothing);
			sut[0, column].Should().Be(2.ToMaybe());
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void When2AtRow0AndColumn_MoveUp_2AtSamePlace(int column)
		{
			var sut = new Game
			{
				[0, column] = 2.ToMaybe()
			};

			sut.Move(Direction.Up);

			sut[0, column].Should().Be(2.ToMaybe());
		}

		[Fact]
		public void When2Along00To33Diagonal_MoveUp_2Along00To03Line()
		{
			var sut = new Game
			{
				[0, 0] = 2.ToMaybe(),
				[1, 1] = 2.ToMaybe(),
				[2, 2] = 2.ToMaybe(),
				[3, 3] = 2.ToMaybe()
			};

			sut.Move(Direction.Up);

			sut[0, 0].Should().Be(2.ToMaybe());
			sut[0, 1].Should().Be(2.ToMaybe());
			sut[0, 2].Should().Be(2.ToMaybe());
			sut[0, 3].Should().Be(2.ToMaybe());
		}
	}
}