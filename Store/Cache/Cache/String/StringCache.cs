using Cache.Basic;

namespace Cache.String
{
    internal class StringCache<TVal>(long? pSizeLimit = null, int? pLifeInSec = null) : BasicCache<string, TVal>(pSizeLimit, pLifeInSec)
    {
        protected override bool IsKeyValid(string pKey)
        {
            return !string.IsNullOrWhiteSpace(pKey);
        }
    }
}
