using System.Data;

using LogWrapper.Loggers;
using LogWrapper.Loggers.Null;

using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Queries.SimpleSelectQuery;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Test.Fakes;
using SqliteDbWrapper.Testing;
using SqliteDbWrapper.Values;
using SqliteDbWrapper.Wrappers;

namespace SqliteDbWrapper.Test.Wrappers
{
    /// <summary>
    /// Tests the SimpleSqliteDbWrapper implementation.
    /// </summary>
    public class SimpleSqliteDbWrapperTest
	{

		public static ISqliteDbWrapper<TModel> CreateWrapper<TModel>(IDbConnection pSqlite)
		{
			// Mock out db so we don't need external connection. No data transfer actually has to occurr
			// Mock out logger so we don't rely on logging to console. Not needed.
			// Use the default SimpleCache

			// Mock out the sqlite connection
			ILogger logger = new NullLoggerFactory().CreateNewLogger(typeof(SimpleSqliteDbWrapperTest));
			ISqliteDbCache<ICollection<TModel>> cache = new TestCache<ICollection<TModel>>();

			return TestWrapperFactory.GetSimpleWrapper(pSqlite, cache, logger);
		}

		/* We can test the DB operations by checking the Command Text.
		 * CommandText is set when we are about to execute the command.
		 * We can safely assume: if the Command Text is correct, then the operation is correct.
		 */
		#region "CreateTable"
		[Fact]
		public void Test_CreateTable_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			ICollection<string> columns = ["column1"];

			// Act
			void action() => wrapper.CreateTable(table, columns);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_CreateTable_ColumnsAreEmpty_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<string> columns = [];

			// Act
			void action() => wrapper.CreateTable(table, columns);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_CreateTable_ExecuteCreateTableQuery()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<string> columns = ["column1"];

			// Act
			wrapper.CreateTable(table, columns);
			string actual = conn.TestCommand.CommandText;
			string expected = "CREATE TABLE table(column1);";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "DropTable"
		[Fact]
		public void Test_DropTable_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";

			// Act
			void action() => wrapper.DropTable(table);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_DropTable_ExecuteDropTableQuery()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";

			// Act
			wrapper.DropTable(table);
			string actual = conn.TestCommand.CommandText;
			string expected = "DROP TABLE table;";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "Insert"
		[Fact]
		public void Test_Insert_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			SqliteDbValueList values = new(["value1"]);
			SqliteDbValueList? columns = null;

			// Act
			void action() => wrapper.Insert(table, values, columns);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_Insert_ValuesIsNull_ThrowArgumentNullException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbValueList? values = null;
			SqliteDbValueList? columns = null;

			// Act
#pragma warning disable CS8604 // Possible null reference argument.
			void action() => wrapper.Insert(table, values, columns);
#pragma warning restore CS8604 // Possible null reference argument.

			// Assert
			Assert.Throws<ArgumentNullException>(action);
		}

		[Fact]
		public void Test_Insert_ExecuteInsertQuery()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbValueList values = new(["value1", "value2"]);
			SqliteDbValueList columns = new(["column1", "column2"]);

			// Act
			wrapper.Insert(table, values, columns);
			string actual = conn.TestCommand.CommandText;
			string expected = "INSERT INTO table (column1, column2) VALUES (value1, value2);";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "InsertAll"
		[Fact]
		public void Test_InsertAll_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			ICollection<SqliteDbValueList> values = [new(["value1"])];
			SqliteDbValueList? columns = null;

			// Act
			void action() => wrapper.InsertAll(table, values, columns);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_InsertAll_ValuesIsNullOrEmpty_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<SqliteDbValueList> values = [];
			SqliteDbValueList? columns = null;

			// Act
			void action() => wrapper.InsertAll(table, values, columns);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_InsertAll_ExecuteInsertQuery()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			ICollection<SqliteDbValueList> values = [new(["value1", "value2"]), new(["2value1", "2value2"])];
			SqliteDbValueList columns = new(["column1", "column2"]);

			// Act
			wrapper.InsertAll(table, values, columns);
			string actual = conn.TestCommand.CommandText;
			string expected = "INSERT INTO table (column1, column2) VALUES (value1, value2), (2value1, 2value2);";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "Update"
		[Fact]
		public void Test_Update_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
			SqliteDbUpdateValues values = new(["column1"], ["value1"]);
			string? where = null;

			// Act
			void action() => wrapper.Update(table, values, where);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_Update_ValuesIsNull_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbUpdateValues? values = null;
			string? where = null;

			// Act
#pragma warning disable CS8604 // Possible null reference argument.
			void action() => wrapper.Update(table, values, where);
#pragma warning restore CS8604 // Possible null reference argument.

			// Assert
			Assert.Throws<ArgumentNullException>(action);
		}

		[Fact]
		public void Test_Update_ExecuteUpdateQuery()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
			SqliteDbUpdateValues values = new(["column1", "column2"], ["value1", "value2"]);
			string where = "column1=1";

			// Act
			wrapper.Update(table, values, where);
			string actual = conn.TestCommand.CommandText;
			string expected = "UPDATE table SET column1 = value1, column2 = value2 WHERE column1=1;";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "SelectFirst"
		[Fact]
		public void Test_SelectFirst_TableIsNullOrWhitespace_ThrowArgumentException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "";
            ISqliteDbDataReader<string> reader = new TestDbMapper();
			bool isForce = false;

			SimpleSqliteDbSelectQuery selectQuery = new();
			selectQuery.Select("value1, value2");

			// Act
			void action() => wrapper.Select(table, selectQuery, reader, isForce);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_SelectFirst_SelectQueryIsNull_ThrowArgumentNullException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
            ISqliteDbDataReader<string> reader = new TestDbMapper();
			bool isForce = false;

			SimpleSqliteDbSelectQuery? selectQuery = null;

			// Act
#pragma warning disable CS8604 // Possible null reference argument.
			void action() => wrapper.Select(table, selectQuery, reader, isForce);
#pragma warning restore CS8604 // Possible null reference argument.

			// Assert
			Assert.Throws<ArgumentNullException>(action);
		}

		[Fact]
		public void Test_SelectFirst_ReaderIsNull_ThrowArgumentNullException()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
            ISqliteDbDataReader<string>? reader = null;
			bool isForce = false;

			SimpleSqliteDbSelectQuery selectQuery = new();
			selectQuery.Select("value1, value2");

			// Act
#pragma warning disable CS8604 // Possible null reference argument.
			void action() => wrapper.Select(table, selectQuery, reader, isForce);
#pragma warning restore CS8604 // Possible null reference argument.

			// Assert
			Assert.Throws<ArgumentNullException>(action);
		}

		[Fact]
		public void Test_SelectFirst_ExecuteSelectQuery()
		{
			// Arrange
			TestDbConnection conn = new();
			ISqliteDbWrapper<string> wrapper = CreateWrapper<string>(conn);
			string table = "table";
            ISqliteDbDataReader<string> reader = new TestDbMapper();
			bool isForce = false;

			SimpleSqliteDbSelectQuery selectQuery = new();
			selectQuery.Select("value1, value2").Where("column1=value1");

			// Act
			wrapper.Select(table, selectQuery, reader, isForce);
			string actual = conn.TestCommand.CommandText;
			string expected = "SELECT value1, value2 FROM table WHERE column1=value1;";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion
	}
}
