using LiteDB;

using LogWrapper.Loggers;

namespace LiteDbWrapper.Wrappers.SimpleLiteDbWrapper
{
	/// <summary>
	/// A simple implementation of a LiteDb wrapper.
	/// LiteDB has a built-in cache.
	/// </summary>
	internal sealed class SimpleLiteDbWrapper : BaseLiteDbWrapper, ILiteDbWrapper
	{
		public SimpleLiteDbWrapper(string pDbPath, ILogger pLogger) : base(pDbPath, pLogger) { }

		public SimpleLiteDbWrapper(ILiteDatabase pDb, ILogger pLogger) : base(pDb, pLogger) { }

		#region "ILiteDbWrapper"
		/// <summary>
		/// Add a single item to the collection.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <param name="pModel"></param>
		/// <returns></returns>
		public void Add<TModel>(string pColName, int pId, TModel pModel)
		{
			Add(pColName, new BsonValue(pId), pModel);
		}

		/// <summary>
		/// Add a single item to the collection.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <param name="pModel"></param>
		public void Add<TModel>(string pColName, Guid pId, TModel pModel)
		{
			Add(pColName, new BsonValue(pId), pModel);
		}

		/// <summary>
		/// Creates a new collection.
		/// </summary>
		/// <param name="pColName"></param>
		public void Create(string pColName)
		{
			log.Debug($"BEGIN: CREATE {pColName}.");

			// LiteDb automatically creates a collection when retrieving it, if it does not already exist.
			_ = _db.GetCollection(pColName);

			log.Debug($"END: CREATE {pColName}.");
		}

		/// <summary>
		/// Deletes an item by id.
		/// </summary>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		public void DeleteById(string pColName, int pId)
		{
			DeleteById(pColName, new BsonValue(pId));
		}

		/// <summary>
		/// Deletes an item by id.
		/// </summary>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		public void DeleteById(string pColName, Guid pId)
		{
			DeleteById(pColName, new BsonValue(pId));
		}

		/// <summary>
		/// Deletes all items in the collection.
		/// </summary>
		/// <param name="pColName"></param>
		public void DeleteAll(string pColName)
		{
			log.Debug($"BEGIN: DELETE ALL {pColName}.");

			ILiteCollection<BsonDocument> col = GetCol(pColName);
			int result = col.DeleteAll();

			log.Debug($"END: DELETE ALL {pColName}: {result}.");
		}

		/// <summary>
		/// Drops the collection.
		/// </summary>
		/// <param name="pColName"></param>
		public void Drop(string pColName)
		{
			log.Debug($"BEGIN: DROP {pColName}.");

			_ = _db.DropCollection(pColName);

			log.Debug($"END: DROP {pColName}.");
		}

		/// <summary>
		/// Gets all in the collection.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <returns></returns>
		public ICollection<TModel> GetAll<TModel>(string pColName)
		{
			log.Debug($"BEGIN: GET ALL {pColName}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			IEnumerable<TModel> result = col.FindAll();

			log.Debug($"END: GET ALL {pColName}.");
			return (ICollection<TModel>)result;
		}

		/// <summary>
		/// Gets one by matching the Id.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <returns></returns>
		public TModel? GetById<TModel>(string pColName, int pId)
		{
			return GetById<TModel>(pColName, new BsonValue(pId));
		}

		/// <summary>
		/// Gets one by matching the Id.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <returns></returns>
		public TModel? GetById<TModel>(string pColName, Guid pId)
		{
			return GetById<TModel>(pColName, new BsonValue(pId));
		}

		/// <summary>
		/// Update an item by id.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <param name="pModel"></param>
		public void UpdateById<TModel>(string pColName, int pId, TModel pModel)
		{
			UpdateById(pColName, new BsonValue(pId), pModel);
		}

		/// <summary>
		/// Update an item by id.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <param name="pModel"></param>
		public void UpdateById<TModel>(string pColName, Guid pId, TModel pModel)
		{
			UpdateById(pColName, new BsonValue(pId), pModel);
		}
		#endregion

		#region "Op Helpers"
		private void Add<TModel>(string pColName, BsonValue pId, TModel pModel)
		{
			log.Debug($"BEGIN: ADD {pId} TO {pColName}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			col.Insert(pId, pModel);

			log.Debug($"END: ADD {pId} TO {pColName}.");
		}

		private void DeleteById(string pColName, BsonValue pId)
		{
			log.Debug($"BEGIN: DELETE {pId} IN {pColName}.");

			ILiteCollection<BsonDocument> col = GetCol(pColName);
			_ = col.Delete(pId);

			log.Debug($"END: DELETE {pId} IN {pColName}.");
		}

		private TModel? GetById<TModel>(string pColName, BsonValue pId)
		{
			log.Debug($"BEGIN: GET {pId}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			TModel? result = col.FindById(pId);

			log.Debug($"END: GET {pId}.");
			return result;
		}

		private void UpdateById<TModel>(string pColName, BsonValue pId, TModel pModel)
		{
			log.Debug($"BEGIN: UPDATE {pId} IN {pColName}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			_ = col.Update(pId, pModel);

			log.Debug($"END: UPDATE {pId} IN {pColName}.");
		}
		#endregion
	}
}
