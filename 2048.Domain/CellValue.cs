using System;
using Functional.Maybe;

namespace _2048
{
	public struct CellValue : IEquatable<CellValue>
	{
		private readonly Maybe<int> _value;

		private CellValue(Maybe<int> value)
		{
			_value = value;
		}

		// TODO : Find a way to not rely on HasValue / Value !
		public bool HasValue => _value.HasValue;
		public int Value => _value.Value;

		public bool Equals(CellValue other) 
			=> _value.Equals(other._value);

		public override bool Equals(object obj) 
			=> obj is CellValue value && Equals(value) 
		    || obj is int intValue && _value.SelectOrElse(v => v == intValue, () => false);

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