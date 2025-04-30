using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Queries.SimpleSelectQuery;

namespace SqliteDbWrapper.Test.Queries
{
    /// <summary>
    /// Tests the SimpleSqliteDbSelectQuery implementation.
    /// </summary>
    public class SimpleSqliteDbSelectQueryTest
	{
		private static SimpleSqliteDbSelectQuery CreateEmptySimpleSelectQuery()
		{
			SimpleSqliteDbSelectQuery query = new();

			return query;
		}

		private static SimpleSqliteDbSelectQuery CreateLoadedSimpleSelectQuery(string? pFields = null, int? pTop = null, bool? pIsDistinct = null, string? pWhere = null, string? pGroupBy = null, string? pOrderBy = null, bool? pIsAsc = null)
		{
			SimpleSqliteDbSelectQuery query = new();

			if (pFields != null)
			{
				query.Select(pFields);
			}

			if (pTop != null)
			{
				query.Top(pTop.Value);
			}

			if (pIsDistinct != null)
			{
				query.Distinct(pIsDistinct.Value);
			}

			if (pWhere != null)
			{
				query.Where(pWhere);
			}

			if (pGroupBy != null)
			{
				query.GroupBy(pGroupBy);
			}

			if (pOrderBy != null)
			{
				query.OrderBy(pOrderBy);
			}

			if (pIsAsc != null)
			{
				query.Asc(pIsAsc.Value);
			}

			return query;
		}

		#region "BuildQuery"
		[Fact]
		public void Test_BuildQuery_EmptyQuery_ReturnSelectAllQuery()
		{
			// Arrange
			SimpleSqliteDbSelectQuery query = CreateEmptySimpleSelectQuery();
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT * FROM {table};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("field1", "field1")]
		[InlineData("field1, field2", "field1, field2")]
		[InlineData("", "*")]
		public void Test_BuildQuery_OnlyFieldsSet_ReturnSelectFieldsQuery(string pFields, string pExpectedFieldsStr)
		{
			// Arrange
			string fields = pFields;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {pExpectedFieldsStr} FROM {table};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(true, "DISTINCT ")]
		[InlineData(false, "")]
		[InlineData(null, "")]
		public void Test_BuildQuery_DistinctFields_ReturnSelectDistinctFieldsQuery(bool? pIsDistinct, string pExpectedDistinctStr)
		{
			// Arrange
			string fields = "fields";
			bool? isDistinct = pIsDistinct;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pIsDistinct: isDistinct);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {pExpectedDistinctStr}{fields} FROM {table};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(1, "TOP 1 ")]
		[InlineData(0, "")]
		[InlineData(-1, "")]
		public void Test_BuildQuery_TopFields_ReturnSelectTopFieldsQuery(int pTop, string pExpectedTopStr)
		{
			// Arrange
			string fields = "fields";
			int top = pTop;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pTop: top);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {pExpectedTopStr}{fields} FROM {table};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Test_BuildQuery_DistinctTopFields_ReturnSelectDistinctTopFieldsQuery()
		{
			// Arrange
			string fields = "fields";
			bool isDistinct = true;
			int top = 1;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pIsDistinct: isDistinct, pTop: top);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT DISTINCT TOP 1 {fields} FROM {table};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("where", " WHERE where")]
		[InlineData("", "")]
		[InlineData(null, "")]
		public void Test_BuildQuery_WithWhere_ReturnSelectFieldsWhereQuery(string? pWhere, string pExpectedWhereStr)
		{
			// Arrange
			string fields = "fields";
			string? where = pWhere;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pWhere: where);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {fields} FROM {table}{pExpectedWhereStr};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("group", " GROUP BY group")]
		[InlineData("", "")]
		[InlineData(null, "")]
		public void Test_BuildQuery_WithGroupBy_ReturnSelectFieldsGroupByQuery(string? pGroupBy, string pExpectedGroupByStr)
		{
			// Arrange
			string fields = "fields";
			string? groupBy = pGroupBy;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pGroupBy: groupBy);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {fields} FROM {table}{pExpectedGroupByStr};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("order", true, " ORDER BY order ASC")]
		[InlineData("order", false, " ORDER BY order DESC")]
		[InlineData("order", null, " ORDER BY order DESC")]
		[InlineData("", true, "")]
		[InlineData("", false, "")]
		[InlineData("", null, "")]
		[InlineData(null, true, "")]
		[InlineData(null, false, "")]
		[InlineData(null, null, "")]
		public void Test_BuildQuery_WithOrderBy_ReturnSelectFieldsOrderByQuery(string? pOrderBy, bool? pIsAsc, string pExpectedOrderByStr)
		{
			// Arrange
			string fields = "fields";
			string? orderBy = pOrderBy;
			bool? isAsc = pIsAsc;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pOrderBy: orderBy, pIsAsc: isAsc);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {fields} FROM {table}{pExpectedOrderByStr};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Test_BuildQuery_WithWhereGroupByOrderByAsc_ReturnSelectFieldsWhereGroupByOrderByAscQuery()
		{
			// Arrange
			string fields = "fields";
			string where = "where";
			string groupBy = "group";
			string orderBy = "order";
			bool isAsc = true;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(pFields: fields, pWhere: where, pGroupBy: groupBy, pOrderBy: orderBy, pIsAsc: isAsc);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT {fields} FROM {table} WHERE {where} GROUP BY {groupBy} ORDER BY {orderBy} {(isAsc ? "ASC" : "DESC")};";

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Test_BuildQuery_WithAll_ReturnSelectDistinctTopFieldsWhereGroupByOrderByAscQuery()
		{
			// Arrange
			string fields = "fields";
			bool isDistinct = true;
			int top = 1;
			string where = "where";
			string groupBy = "group";
			string orderBy = "order";
			bool isAsc = true;
			SimpleSqliteDbSelectQuery query = CreateLoadedSimpleSelectQuery(fields, top, isDistinct, where, groupBy, orderBy, isAsc);
			string table = "table";

			// Act
			string actual = query.BuildQuery(table);
			string expected = $"SELECT DISTINCT TOP {top} {fields} FROM {table} WHERE {where} GROUP BY {groupBy} ORDER BY {orderBy} ASC;";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion
	}
}
