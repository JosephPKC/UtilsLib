using System.Data;

using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Test.Fakes
{
	public class TestDbMapper : ISqliteDbReader<string>
	{
		public ICollection<string> ReadAll(IDataReader pReader)
		{
			return ["value1", "value2"];
		}

		public string? ReaderFirst(IDataReader pReader)
		{
			return "value1";
		}
	}
}
