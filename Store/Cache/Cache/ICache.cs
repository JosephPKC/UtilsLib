namespace Cache
{
    public interface ICache<TKey, TVal>
    {
        void Clear();
        bool Delete(TKey pKey);
        bool Exists(TKey pKey);
        TVal? Get(TKey pKey);
        bool Put(TKey pKey, TVal pVal, int? pLifeInSec = null, bool pOverwrite = false);
    }
}
