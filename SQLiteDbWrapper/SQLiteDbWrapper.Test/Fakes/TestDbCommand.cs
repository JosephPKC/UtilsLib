using System.Data;

namespace SqliteDbWrapper.Test.Fakes
{
	public class TestDbCommand : IDbCommand
	{
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
		public string CommandText { get; set; } = "command";
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
		public int CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public CommandType CommandType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public IDbConnection? Connection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public IDataParameterCollection Parameters => throw new NotImplementedException();

		public IDbTransaction? Transaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public void Cancel()
		{
			throw new NotImplementedException();
		}

		public IDbDataParameter CreateParameter()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			// Do Nothing
			GC.SuppressFinalize(this);
		}

		public int ExecuteNonQuery()
		{
			return 1;
		}

		public IDataReader ExecuteReader()
		{
			return new TestDataReader();
		}

		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			return new TestDataReader();
		}

		public object? ExecuteScalar()
		{
			throw new NotImplementedException();
		}

		public void Prepare()
		{
			throw new NotImplementedException();
		}
	}
}
