using SqliteDbWrapper;
using SqliteDbWrapper.Wrappers;

namespace SqliteDbWrapper.Test.Wrappers
{
    public class SqliteDbWrapperFactoryTest
	{
		public static ISqliteDbWrapperFactory CreateWrapperFactory()
		{
			return new SqliteDbWrapperFactory();
		}

		#region "CreateNewWrapper"
		[Fact]
		public void Test_CreateNewWrapper_NoConfigs_ReturnSimpleWrapper()
		{
			// Arrange
			ISqliteDbWrapperFactory factory = CreateWrapperFactory();
			string dbFilePath = "";
			bool isNew = false;
			SqliteDbWrapperConfigs? configs = null;

			// Act
			ISqliteDbWrapper<string> actual = factory.CreateNewWrapper<string>(dbFilePath, isNew, configs);
			string actualTypeName = actual.GetType().Name;
			string expected = "SimpleSqliteDbWrapper`1"; // 1 Generic Param

			// Assert
			Assert.Equal(expected, actualTypeName);
		}

		[Fact]
		public void Test_CreateNewWrapper_ExtensiveLoggingEnabled_ReturnLoggedWrapper()
		{
			// Arrange
			ISqliteDbWrapperFactory factory = CreateWrapperFactory();
			string dbFilePath = "";
			bool isNew = false;
			SqliteDbWrapperConfigs configs = new()
			{
				IsUseExtensiveLogging = true
			};

			// Act
			ISqliteDbWrapper<string> actual = factory.CreateNewWrapper<string>(dbFilePath, isNew, configs);
			string actualTypeName = actual.GetType().Name;
			string expected = "LoggedSqliteDbWrapper`1"; // 1 Generic Param

			// Assert
			Assert.Equal(expected, actualTypeName);
		}

		[Fact]
		public void Test_CreateNewWrapper_NoSpecialConfigsOrDefault_ReturnSimpleWrapper()
		{
			// Arrange
			ISqliteDbWrapperFactory factory = CreateWrapperFactory();
			string dbFilePath = "";
			bool isNew = false;
			SqliteDbWrapperConfigs configs = new()
			{
				IsUseExtensiveLogging = false
			};

			// Act
			ISqliteDbWrapper<string> actual = factory.CreateNewWrapper<string>(dbFilePath, isNew, configs);
			string actualTypeName = actual.GetType().Name;
			string expected = "SimpleSqliteDbWrapper`1"; // 1 Generic Param

			// Assert
			Assert.Equal(expected, actualTypeName);
		}
		#endregion
	}
}
