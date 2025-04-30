using System.Data;

namespace SqliteDbWrapper.Test.Fakes
{
	public class TestDbConnection : IDbConnection
	{
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
		public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
		public int ConnectionTimeout => throw new NotImplementedException();

		public string Database => throw new NotImplementedException();

		public ConnectionState State => throw new NotImplementedException();

		public IDbTransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			throw new NotImplementedException();
		}

		public void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			// Do Nothing
		}

		public IDbCommand CreateCommand()
		{
			return TestCommand;
		}

		public void Dispose()
		{
			// Do Nothing
			GC.SuppressFinalize(this);
		}

		public void Open()
		{
			// Do Nothing
		}

		private TestDbCommand? _testCommand = null;
		public TestDbCommand TestCommand {
			get
			{
				_testCommand ??= new TestDbCommand();
				return _testCommand;
			}
		}
		public string CommandTextExp => TestCommand.CommandText;
	}
}
