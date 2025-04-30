namespace Cache
{
    public interface ICacheFactory
    {
        ICache<TKey, TVal> BuildCache<TKey, TVal>(long? pSizeLimit = null, int? pLifeInSec = null);
    }
}
