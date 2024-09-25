using SqliteDbWrapper.Wrappers;

namespace SqliteDbWrapper
{
    public interface ISqliteDbWrapperFactory
    {
        ISqliteDbWrapper<TBaseDbModel> CreateNewWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null);
    }
}
