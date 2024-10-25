using LiteDB;

using LogWrapper.Loggers;

namespace LiteDbWrapper.Wrappers
{
	internal abstract class BaseLiteDbWrapper
	{
		protected readonly ILogger log;
		protected readonly ILiteDatabase _db;

		public BaseLiteDbWrapper(string pDbPath, ILogger pLogger)
		{
			_db = new LiteDatabase(pDbPath);
			log = pLogger;
		}

		public BaseLiteDbWrapper(ILiteDatabase pDb, ILogger pLogger)
		{
			_db = pDb;
			log = pLogger;
		}

		protected virtual ILiteCollection<TModel> GetCol<TModel>(string pColName) where TModel : LiteDbModel
		{
			try
			{
				ILiteCollection<TModel> col = _db.GetCollection<TModel>(pColName);
				return col;
			}
			catch (Exception ex)
			{
				log.Error(ex);
				throw;
			}
		}

		protected virtual ILiteCollection<BsonDocument> GetCol(string pColName)
		{
			try
			{
				ILiteCollection<BsonDocument> col = _db.GetCollection(pColName);
				return col;
			}
			catch (Exception ex)
			{
				log.Error(ex);
				throw;
			}
		}
	}
}
