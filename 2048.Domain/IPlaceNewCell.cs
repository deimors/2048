namespace _2048
{
	public interface IPlaceNewCell
	{
		int ChoosePositionIndex(int max);
		int ChooseValue();
	}
}