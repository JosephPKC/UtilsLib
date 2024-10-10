using SqliteDbWrapper.Wrappers;

namespace SqliteDbWrapper
{
    public interface ISqliteDbWrapperFactory
    {
        ISqliteDbWrapper<TBaseDbModel> CreateNewWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null);
		ISqliteDbReader<TBaseDbModel> CreateNewReader<TBaseDbModel>(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null);
		ISqliteDbWriter CreateNewWriter(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null);
	}
}
