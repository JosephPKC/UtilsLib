namespace LiteDbWrapper.Wrappers
{
	public interface ILiteDbWrapper
	{
		void Add<TModel>(string pColName, int pId, TModel pModel);
		void Add<TModel>(string pColName, Guid pId, TModel pModel);
		void DeleteById(string pColName, int pId);
		void DeleteById(string pColName, Guid pId);
		void DeleteAll(string pColName);
		void Drop(string pColName);
		ICollection<TModel> GetAll<TModel>(string pColName);
		TModel? GetById<TModel>(string pColName, int pId);
		TModel? GetById<TModel>(string pColName, Guid pId);
		void UpdateById<TModel>(string pColName, int pId, TModel pModel);
		void UpdateById<TModel>(string pColName, Guid pId, TModel pModel);
	}
}
