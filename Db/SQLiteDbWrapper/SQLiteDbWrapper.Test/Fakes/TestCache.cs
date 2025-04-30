using Cache;

namespace SqliteDbWrapper.Test.Fakes
{
	public class TestCache<TItem> : ICache<string, TItem>
	{
        public void Clear()
        {
            
        }

        public bool Delete(string pKey)
        {
            return true;
        }

        public bool Exists(string pKey)
        {
            return true;
        }

        public void Flush()
		{
			
		}

        public TItem? Get(string pKey)
        {
            return default;
        }

        public bool Put(string pKey, TItem pVal, int? pLifeInSec = null, bool pOverwrite = false)
        {
            return true;
        }
	}
}
