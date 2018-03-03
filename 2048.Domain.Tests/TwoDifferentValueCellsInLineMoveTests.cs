﻿using FluentAssertions;
using Functional.Maybe;
using Xunit;

namespace _2048.Domain.Tests
{
	public class TwoDifferentValueCellsInLineMoveTests
	{
		[Fact]
		public void When2At00And4At01_MoveRight_2At02And4At03()
		{
			var sut = new Game
			{
				[0, 0] = 2.ToMaybe(),
				[0, 1] = 4.ToMaybe()
			};

			sut.Move(Direction.Right);

			sut[0, 2].Should().Be(2.ToMaybe());
			sut[0, 3].Should().Be(4.ToMaybe());
		}

		[Fact]
		public void When2At00And4At10_MoveDown_2At20And4At30()
		{
			var sut = new Game
			{
				[0, 0] = 2.ToMaybe(),
				[1, 0] = 4.ToMaybe()
			};

			sut.Move(Direction.Down);

			sut[2, 0].Should().Be(2.ToMaybe());
			sut[3, 0].Should().Be(4.ToMaybe());
		}

		[Fact]
		public void When2At02And4At03_MoveLeft_2At00And4At01()
		{
			var sut = new Game
			{
				[0, 2] = 2.ToMaybe(),
				[0, 3] = 4.ToMaybe()
			};

			sut.Move(Direction.Left);

			sut[0, 0].Should().Be(2.ToMaybe());
			sut[0, 1].Should().Be(4.ToMaybe());
		}

		[Fact]
		public void When2At20And4At30_MoveUp_2At00And4At10()
		{
			var sut = new Game
			{
				[2, 0] = 2.ToMaybe(),
				[3, 0] = 4.ToMaybe()
			};

			sut.Move(Direction.Up);

			sut[0, 0].Should().Be(2.ToMaybe());
			sut[1, 0].Should().Be(4.ToMaybe());
		}
	}
}