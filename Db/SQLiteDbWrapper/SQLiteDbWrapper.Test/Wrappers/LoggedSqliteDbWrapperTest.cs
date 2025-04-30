using System.Data;

using FluentAssertions;

using Cache;
using LogWrapper.Loggers;
using LogWrapper.Loggers.Null;

using SqliteDbWrapper.Queries.SimpleSelectQuery;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Test.Fakes;
using SqliteDbWrapper.Testing;
using SqliteDbWrapper.Values;
using SqliteDbWrapper.Wrappers;

namespace SqliteDbWrapper.Test.Wrappers
{
    /// <summary>
    /// Tests the LoggedSqliteDbWrapper implementation.
    /// </summary>
    public class LoggedSqliteDbWrapperTest
	{

		public static ISqliteDbWrapper<TModel> CreateWrapper<TModel>(IDbConnection pSqlite)
		{
			// Mock out db so we don't need external connection. No data transfer actually has to occurr
			// Mock out logger so we don't rely on logging to console. Not needed.
			// Use the default SimpleCache

			// Mock out the sqlite connection
			ILogger logger = new NullLoggerFactory().CreateNewLogger(typeof(LoggedSqliteDbWrapperTest));
			ICache<string, ICollection<TModel>> cache = new TestCache<ICollection<TModel>>();

			return TestWrapperFactory.GetLoggedWrapper(pSqlite, cache, logger);
		}

		/* We can test the DB operations by checking the Command Text.
		 * CommandText is set when we are about to execute the command.
		 * We can safely assume: if the Command Text is correct, then the operation is correct.
		 */
		#region CreateTable
		[Fact]
		public void CreateTable_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			ICollection<string> columns = ["column1"];

			Action act = void() => wrapper.CreateTable(table, columns);

			act.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void CreateTable_ColumnsAreEmpty_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<string> columns = [];

			Action act = void() => wrapper.CreateTable(table, columns);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void CreateTable_ExecuteCreateTableQuery()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<string> columns = ["column1"];

			wrapper.CreateTable(table, columns);
			string actual = conn.TestCommand.CommandText;
			string expected = "CREATE TABLE table(column1);";

			actual.Should().Be(expected);
		}
		#endregion

		#region DropTable
		[Fact]
		public void DropTable_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";

			Action act = void() => wrapper.DropTable(table);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void DropTable_ExecuteDropTableQuery()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";

			wrapper.DropTable(table);
			string actual = conn.TestCommand.CommandText;
			string expected = "DROP TABLE table;";

			actual.Should().Be(expected);
		}
		#endregion

		#region Insert
		[Fact]
		public void Insert_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			SqliteDbValueList values = new(["value1"]);
			SqliteDbValueList? columns = null;

			Action act = void() => wrapper.Insert(table, values, columns);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void Insert_ValuesIsNull_ThrowArgumentNullException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbValueList? values = null;
			SqliteDbValueList? columns = null;

			Action act = void() => wrapper.Insert(table, values!, columns);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void Insert_ExecuteInsertQuery()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbValueList values = new(["value1", "value2"]);
			SqliteDbValueList columns = new(["column1", "column2"]);

			wrapper.Insert(table, values, columns);
			string actual = conn.TestCommand.CommandText;
			string expected = "INSERT INTO table (column1, column2) VALUES (value1, value2);";

			actual.Should().Be(expected);
		}
		#endregion

		#region InsertAll
		[Fact]
		public void InsertAll_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			ICollection<SqliteDbValueList> values = [new(["value1"])];
			SqliteDbValueList? columns = null;

			Action act = void() => wrapper.InsertAll(table, values, columns);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void InsertAll_ValuesIsNullOrEmpty_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<SqliteDbValueList> values = [];
			SqliteDbValueList? columns = null;

			Action act = void() => wrapper.InsertAll(table, values, columns);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void InsertAll_ExecuteInsertQuery()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<SqliteDbValueList> values = [new(["value1", "value2"]), new(["2value1", "2value2"])];
			SqliteDbValueList columns = new(["column1", "column2"]);

			wrapper.InsertAll(table, values, columns);
			string actual = conn.TestCommand.CommandText;
			string expected = "INSERT INTO table (column1, column2) VALUES (value1, value2), (2value1, 2value2);";

			actual.Should().Be(expected);
		}
		#endregion

		#region Update
		[Fact]
		public void Update_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			SqliteDbUpdateValues values = new(["column1"], ["value1"]);
			string? where = null;

			Action act = void() => wrapper.Update(table, values, where);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void Update_ValuesIsNull_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbUpdateValues? values = null;
			string? where = null;

			Action act = void() => wrapper.Update(table, values!, where);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void Update_ExecuteUpdateQuery()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbUpdateValues values = new(["column1", "column2"], ["value1", "value2"]);
			string where = "column1=1";

			wrapper.Update(table, values, where);
			string actual = conn.TestCommand.CommandText;
			string expected = "UPDATE table SET column1 = value1, column2 = value2 WHERE column1=1;";

			actual.Should().Be(expected);	
		}
		#endregion

		#region SelectFirst
		[Fact]
		public void SelectFirst_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
            Readers.ISqliteDbDataReader<string> reader = new TestDbMapper();
			bool isForce = false;

			SimpleSqliteDbSelectQuery selectQuery = new();
			selectQuery.Select("value1, value2");

			Action act = void() => wrapper.Select(table, selectQuery, reader, isForce);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void SelectFirst_SelectQueryIsNull_ThrowArgumentNullException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
            Readers.ISqliteDbDataReader<string> reader = new TestDbMapper();
			bool isForce = false;

			SimpleSqliteDbSelectQuery? selectQuery = null;

			Action act = void() => wrapper.Select(table, selectQuery!, reader, isForce);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void SelectFirst_ReaderIsNull_ThrowArgumentNullException()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
            ISqliteDbDataReader<string>? reader = null;
			bool isForce = false;

			SimpleSqliteDbSelectQuery selectQuery = new();
			selectQuery.Select("value1, value2");

			Action act = void() => wrapper.Select(table, selectQuery, reader!, isForce);

            act.Should().Throw<ArgumentException>();
        }

		[Fact]
		public void SelectFirst_ExecuteSelectQuery()
		{
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
            Readers.ISqliteDbDataReader<string> reader = new TestDbMapper();
			bool isForce = false;

			SimpleSqliteDbSelectQuery selectQuery = new();
			selectQuery.Select("value1, value2").Where("column1=value1");

			wrapper.Select(table, selectQuery, reader, isForce);
			string actual = conn.TestCommand.CommandText;
			string expected = "SELECT value1, value2 FROM table WHERE column1=value1;";

			actual.Should().Be(expected);
		}
		#endregion
	}
}
