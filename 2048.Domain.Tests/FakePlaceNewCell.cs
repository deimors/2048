namespace _2048.Domain.Tests
{
	public class FakePlaceNewCell : IPlaceNewCell
	{
		private readonly int _position;
		private readonly int _value;

		public FakePlaceNewCell(int position, int value)
		{
			_position = position;
			_value = value;
		}

		public int ChoosePositionIndex(int max) => _position;

		public int ChooseValue() => _value;
	}
}
