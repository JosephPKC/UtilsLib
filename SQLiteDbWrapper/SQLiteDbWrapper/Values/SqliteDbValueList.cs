using Utils.Exceptions;

namespace SqliteDbWrapper.Values
{
	/// <summary>
	/// Data structure that encapuslates lists of strings that represent values or columns (or anything else related to SQLite db operations).
	/// </summary>
	/// <param name="pValues"></param>
	public class SqliteDbValueList(ICollection<string> pValues)
	{
		protected readonly IList<string> _values = [.. pValues];

		public string this[int pIndex] 
		{
			get
			{
				ArgumentOutOfRangeExceptionExt.ThrowIfOutOfRange(_values, pIndex);
				return _values[pIndex];
			}
		}

		public override string ToString()
		{
			return string.Join(", ", _values);
		}
	}
}
