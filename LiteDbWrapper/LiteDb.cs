using LiteDB;
using ColorConsoleLogger;

namespace LiteDbWrapper
{
    public class LiteDb(string pDbPath)
    {
        protected readonly ColorConsole log = new(typeof(LiteDb));
        protected readonly LiteDatabase _db = new(pDbPath);

        /* Public Access Methods */
        public int GetCount(string pColName)
        {
            log.Info($"Get Count {pColName}.");

            ILiteCollection<BsonDocument> col = GetCol<BsonDocument>(pColName);
            int count = col.Count();

            log.Debug($"Get Count {pColName}: {count}.");
            return count;
        }

        public IEnumerable<TModel> GetAll<TModel>(string pColName, string pWhere = "", string pOrder = "", int pLimit = 0) where TModel : LiteDbModel
        {
            string queryStr = LiteDbStringBuilder.BuildQueryString(pColName, pWhere, pOrder, pLimit);
            log.Info($"Get All {queryStr}.");

            ILiteCollection<TModel> col = GetCol<TModel>(pColName);
            ILiteQueryable<TModel> query = col.Query();

            if (!string.IsNullOrWhiteSpace(pWhere))
            {
                query = query.Where(BsonExpression.Create(pWhere));
            }

            if (!string.IsNullOrWhiteSpace(pOrder))
            {
                query = query.OrderBy(BsonExpression.Create(pOrder));
            }

            ILiteQueryableResult<TModel> result = pLimit > 0 ? query : query.Limit(pLimit);
            IEnumerable<TModel> resultList = result.ToList();

            log.Debug($"Get All {queryStr}: {LiteDbStringBuilder.BuildListString(resultList)}.");
            return resultList;
        }

        public TModel? GetFirst<TModel>(string pColName, string pExp) where TModel : LiteDbModel
        {
            string queryStr = LiteDbStringBuilder.BuildQueryString(pColName, pExp, "", 0);
            log.Info($"Get First {queryStr}.");

            ILiteCollection<TModel> col = GetCol<TModel>(pColName);
            TModel? result = col.FindOne(BsonExpression.Create(pExp));

            log.Debug($"Get First {queryStr}: {result}.");
            return result;
        }

        public int DeleteAll<TModel>(string pColName) where TModel : LiteDbModel
        {
            log.Info($"Delete All {pColName}.");

            ILiteCollection<TModel> col = GetCol<TModel>(pColName);
            int result = col.DeleteAll();

            log.Debug($"Delete All {pColName}: {result}.");
            return result;
        }

        public IEnumerable<BsonValue> Insert<TModel>(string pColName, IEnumerable<TModel> pToAdd, Action<TModel>? pPreProcess = null, Action<ILiteCollection<TModel>>? pPostProcessCol = null) where TModel : LiteDbModel
        {
            string listStr = LiteDbStringBuilder.BuildListString(pToAdd);
            log.Info($"Insert {pColName}: {listStr}.");

            ICollection<BsonValue> docIds = [];
            ILiteCollection<TModel> col = GetCol<TModel>(pColName);
            foreach (TModel model in pToAdd)
            {
                BsonValue? result = Insert(col, model, pPreProcess);
                if (result != null)
                {
                    docIds.Add(result);
                }
            }
            pPostProcessCol?.Invoke(col);

            log.Debug($"Insert {pColName}: {listStr}: {LiteDbStringBuilder.BuildListString(docIds)}.");
            return docIds;
        }

        public BsonValue? Insert<TModel>(string pColName, TModel pToAdd, Action<TModel>? pPreProcess = null) where TModel : LiteDbModel
        {
            log.Info($"Insert {pColName}: {pToAdd}.");

            ILiteCollection<TModel> col = GetCol<TModel>(pColName);
            BsonValue? result = Insert(col, pToAdd, pPreProcess);

            log.Debug($"Insert {pColName}: {pToAdd}: {result}.");
            return result;
        }

        public ILiteCollection<BsonDocument> Create(string pColName, BsonAutoId pAutoId = BsonAutoId.ObjectId)
        {
            log.Info($"Create {pColName}.");

            ILiteCollection<BsonDocument> result = _db.GetCollection(pColName, pAutoId);
  
            log.Debug($"Create {pColName}: {result}.");
            return result;
        }

        #region "Collection Helpers"
        protected ILiteCollection<TModel> GetCol<TModel>(string pColName)
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

        protected BsonValue? Insert<TModel>(ILiteCollection<TModel> pCol, TModel pToAdd, Action<TModel>? pPreProcess = null) where TModel : LiteDbModel
        {
            log.Info($"Insert {pCol.Name}: {pToAdd}.");

            if (pToAdd == null)
            {
                return null;
            }

            pPreProcess?.Invoke(pToAdd);
            BsonValue result = pCol.Insert(pToAdd);

            log.Debug($"Insert {pCol.Name}: {pToAdd}: {result}.");
            return result;
        }
        #endregion
    }
}
