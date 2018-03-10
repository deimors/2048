namespace _2048.Domain.Tests
{
	public class FakeNewCellChooser : INewCellChooser
	{
		private readonly int _position;
		private readonly int _value;

		public FakeNewCellChooser(int position, int value)
		{
			_position = position;
			_value = value;
		}

		public int ChoosePositionIndex(int max) => _position;

		public int ChooseValue() => _value;
	}
}
