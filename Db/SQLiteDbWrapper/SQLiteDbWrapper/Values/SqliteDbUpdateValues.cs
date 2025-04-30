using ExceptionExts;

namespace SqliteDbWrapper.Values
{
	/// <summary>
	/// Data structure for columns and values for an UPDATE command.
	/// </summary>
	/// <param name="pValues"></param>
	public class SqliteDbUpdateValues
	{
		protected readonly IList<string> _columns;
		protected readonly IList<string> _values;

		public SqliteDbUpdateValues(ICollection<string> pColumns, ICollection<string> pValues)
		{
			pColumns.ThrowIf().NullOrEmpty();
			pValues.ThrowIf().NullOrEmpty();
			if (pColumns is not null && pColumns.Count != pValues.Count)
			{
				throw new ArgumentException($"Columns and Values must be equal length, if columns is specified.");
			}

			_columns = [.. pColumns!];
			_values = [.. pValues];
		}

		public override string ToString()
		{
			ICollection<string> colValues = [];
			for (int i = 0; i < _values.Count; i++)
			{
				colValues.Add($"{_columns[i]} = {_values[i]}");
			}

			return string.Join(", ", colValues);
		}
	}
}
