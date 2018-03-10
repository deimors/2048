namespace _2048
{
	public interface INewCellChooser
	{
		int ChoosePositionIndex(int max);
		int ChooseValue();
	}
}