using System;
using Functional.Maybe;

namespace _2048
{
	public struct CellValue : IEquatable<CellValue>, IEquatable<int>
	{
		private readonly Maybe<int> _value;

		private CellValue(Maybe<int> value)
		{
			_value = value;
		}
		
		public bool IsEmpty => !_value.HasValue;

		public bool Equals(CellValue other) 
			=> _value.Equals(other._value);

		public bool Equals(int other)
			=> _value.SelectOrElse(value => value == other, () => false);

		public override bool Equals(object obj) 
			=> obj is CellValue cellValue && Equals(cellValue) 
		    || obj is int intValue && Equals(intValue);

		public override int GetHashCode() 
			=> _value.GetHashCode();

		public override string ToString()
			=> _value.SelectOrElse(value => value.ToString(), () => "Empty");

		public static readonly CellValue Empty = new CellValue(Maybe<int>.Nothing);

		public static implicit operator CellValue(int value)
			=> new CellValue(value.ToMaybe());
		
		public TResult Match<TResult>(Func<int, TResult> whenSome, Func<TResult> whenNone)
			=> _value.SelectOrElse(whenSome, whenNone);

		public void Apply(Action<int> whenSome, Action whenNone)
			=> _value.Match(whenSome, whenNone);
	}
}