using SqliteDbWrapper.Cache;

namespace SqliteDbWrapper.Test.Fakes
{
	public class TestCache<TItem> : ISqliteDbCache<TItem>
	{
		public void Flush()
		{
			throw new NotImplementedException();
		}

		public TItem? Retrieve(string pKey)
		{
			return default;
		}

		public bool Store(string pKey, TItem pItem, int pLifeInS)
		{
			return true;
		}

		public bool Update(string pKey, TItem pItem, int pLifeInS)
		{
			return true;
		}
	}
}
