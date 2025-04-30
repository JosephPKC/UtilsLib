using FluentAssertions;

using SqliteDbWrapper.Wrappers;

namespace SqliteDbWrapper.Test.Wrappers
{
    public class SqliteDbWrapperFactoryTest
	{
		public static ISqliteDbWrapperFactory CreateWrapperFactory()
		{
			return new SqliteDbWrapperFactory();
		}

		#region CreateNewWrapper
		[Fact]
		public void CreateNewWrapper_NoConfigs_ReturnSimpleWrapper()
		{
			ISqliteDbWrapperFactory factory = CreateWrapperFactory();
			string dbFilePath = "";
			bool isNew = false;
			SqliteDbWrapperConfigs? configs = null;

			ISqliteDbWrapper<string> actual = factory.CreateNewWrapper<string>(dbFilePath, isNew, configs);
			string actualTypeName = actual.GetType().Name;
			string expected = "SimpleSqliteDbWrapper`1"; // 1 Generic Param

			actualTypeName.Should().Be(expected);
		}

		[Fact]
		public void CreateNewWrapper_ExtensiveLoggingEnabled_ReturnLoggedWrapper()
		{
			ISqliteDbWrapperFactory factory = CreateWrapperFactory();
			string dbFilePath = "";
			bool isNew = false;
			SqliteDbWrapperConfigs configs = new()
			{
				IsUseExtensiveLogging = true
			};

			ISqliteDbWrapper<string> actual = factory.CreateNewWrapper<string>(dbFilePath, isNew, configs);
			string actualTypeName = actual.GetType().Name;
			string expected = "LoggedSqliteDbWrapper`1"; // 1 Generic Param

			actualTypeName.Should().Be(expected);
		}

		[Fact]
		public void CreateNewWrapper_NoSpecialConfigsOrDefault_ReturnSimpleWrapper()
		{
			ISqliteDbWrapperFactory factory = CreateWrapperFactory();
			string dbFilePath = "";
			bool isNew = false;
			SqliteDbWrapperConfigs configs = new()
			{
				IsUseExtensiveLogging = false
			};

			ISqliteDbWrapper<string> actual = factory.CreateNewWrapper<string>(dbFilePath, isNew, configs);
			string actualTypeName = actual.GetType().Name;
			string expected = "SimpleSqliteDbWrapper`1"; // 1 Generic Param

			actualTypeName.Should().Be(expected);
		}
		#endregion
	}
}
