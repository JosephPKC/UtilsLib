﻿using LiteDB;

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
		/// Gets all in the collection.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <returns></returns>
		public ICollection<TModel> GetAll<TModel>(string pColName) where TModel : LiteDbModel
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
		public TModel? GetById<TModel>(string pColName, int pId) where TModel : LiteDbModel
		{
			log.Debug($"BEGIN: GET {pId}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			TModel? result = col.FindById(new BsonValue(pId));

			log.Debug($"END: GET {pId}.");
			return result;
		}

		/// <summary>
		/// Add a single item to the collection.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pModel"></param>
		/// <returns></returns>
		public void Add<TModel>(string pColName, TModel pModel) where TModel : LiteDbModel
		{
			log.Debug($"BEGIN: ADD {pModel.Id} TO {pColName}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			col.Insert(pModel.Id, pModel);

			log.Debug($"END: ADD {pModel.Id} TO {pColName}.");
		}

		/// <summary>
		/// Add a collection of items to the collection.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pModels"></param>
		public void AddAll<TModel>(string pColName, ICollection<TModel> pModels) where TModel : LiteDbModel
		{
			log.Debug($"BEGIN: ADD ALL {pColName}.");
			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			foreach (TModel model in pModels)
			{
				log.Debug($"ADDING {model.Id} TO {pColName}.");
				col.Insert(model.Id, model);
			}
			log.Debug($"END: ADD ALL {pColName}.");
		}

		/// <summary>
		/// Update an item by id.
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		/// <param name="pModel"></param>
		public void UpdateById<TModel>(string pColName, int pId, TModel pModel) where TModel : LiteDbModel
		{
			log.Debug($"BEGIN: UPDATE {pId} IN {pColName}.");

			ILiteCollection<TModel> col = GetCol<TModel>(pColName);
			_ = col.Update(pId, pModel);

			log.Debug($"END: UPDATE {pId} IN {pColName}.");
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
		/// Deletes an item by id.
		/// </summary>
		/// <param name="pColName"></param>
		/// <param name="pId"></param>
		public void DeleteById(string pColName, int pId)
		{
			log.Debug($"BEGIN: DELETE {pId} IN {pColName}.");

			ILiteCollection<BsonDocument> col = GetCol(pColName);
			_ = col.Delete(new BsonValue(pId));

			log.Debug($"END: DELETE {pId} IN {pColName}.");
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
		#endregion
	}
}