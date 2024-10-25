namespace LiteDbWrapper.Wrappers
{
	public interface ILiteDbWrapper
	{
		ICollection<TModel> GetAll<TModel>(string pColName) where TModel : LiteDbModel;
		TModel? GetById<TModel>(string pColName, int pId) where TModel : LiteDbModel;

		void Add<TModel>(string pColName, TModel pModel) where TModel : LiteDbModel;
		void AddAll<TModel>(string pColName, ICollection<TModel> pModels) where TModel: LiteDbModel;

		void UpdateById<TModel>(string pColName, int pId, TModel pModel) where TModel : LiteDbModel;

		void Drop(string pColName);

		void DeleteById(string pColName, int pId);
		void DeleteAll(string pColName);
		
	}
}
