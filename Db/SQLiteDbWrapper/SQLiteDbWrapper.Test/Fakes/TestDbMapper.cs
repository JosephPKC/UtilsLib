using System.Data;

using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Test.Fakes
{
	public class TestDbMapper : ISqliteDbDataReader<string>
	{
		public ICollection<string> ReadAll(IDataReader pReader)
		{
			return ["value1", "value2"];
		}

		public string? ReadFirst(IDataReader pReader)
		{
			return "value1";
		}
	}
}
