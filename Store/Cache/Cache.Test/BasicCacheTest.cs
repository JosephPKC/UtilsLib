using FluentAssertions;

using Cache.Test.Fakes;

namespace Cache.Test
{
	public class BasicCacheTest
	{
        #region Clear
        [Fact]
        public void Clear_LoadedCache_EmptyCache()
        {
            string key = "key";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            cache.Clear();
            string? actual = cache.Get(key);

            actual.Should().BeNull();
        }
        #endregion

        #region Delete
        [Fact]
        public void Delete_ValidKey_ItemFound_ReturnTrueDeleteItem()
        {
            string key = "key";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            bool result = cache.Delete(key);
            string? actual = cache.Get(key);

            result.Should().BeTrue();
            actual.Should().BeNull();
        }

        [Fact]
        public void Delete_ValidKey_ItemNotFound_ReturnFalse()
        {
            string key = "key";
            string key2 = "key2";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            bool result = cache.Delete(key2);

            result.Should().BeFalse();
        }

        [Fact]
        public void Delete_InvalidKey_ReturnFalse()
        {
            string? key = "key";
            string? key2 = null;
            string value = "value";
            ICache<string?, string> cache = TestBasicCacheFactory.CreateLoadedCache<string?, string>(key, value);

            bool result = cache.Delete(key2);

            result.Should().BeFalse();
        }
        #endregion

        #region Exists
        [Fact]
        public void Exists_ItemFound_ReturnTrue()
        {
            string key = "key";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            bool actual = cache.Exists(key);

            actual.Should().BeTrue();
        }

        [Fact]
        public void Exists_ItemNotFound_ReturnFalse()
        {
            string key = "key";
            string key2 = "key2";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            bool actual = cache.Exists(key2);

            actual.Should().BeFalse();
        }
        #endregion

        #region Get
        [Fact]
        public void Get_ValidKey_ReturnVal()
        {
            string key = "key";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            string expected = value;
            string? actual = cache.Get(key);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Get_InvalidKey_ReturnNull()
        {
            string? key = "key";
            string? key2 = null;
            string value = "value";
            ICache<string?, string> cache = TestBasicCacheFactory.CreateLoadedCache<string?, string>(key, value);

            string? actual = cache.Get(key2);

            actual.Should().BeNull();
        }

        [Fact]
        public void Get_ItemNotFound_ReturnNull()
        {
            string key = "key";
            string key2 = "key2";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            string? actual = cache.Get(key2);

            actual.Should().BeNull();
        }
        #endregion

        #region Put
        [Fact]
        public void Put_ValidKey_ValNotFound_ReturnTrueAddVal()
        {
            string key = "key";
            string value = "value";
            ICache<string, string> cache = TestBasicCacheFactory.CreateEmptyCache<string, string>();

            string expected = value;
            bool actual = cache.Put(key, value);
            string? actVal = cache.Get(key);

            actual.Should().BeTrue();
            actVal.Should().Be(expected);
        }

        [Fact]
        public void Put_ValidKey_ValFound_Overwrite_ReturnTrue()
        {
            string key = "key";
            string value = "value";
            string value2 = "value2";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            string expected = value2;
            bool actual = cache.Put(key, value2, pOverwrite: true);
            string? actVal = cache.Get(key);

            actual.Should().BeTrue();
            actVal.Should().Be(expected);
        }

        [Fact]
        public void Put_ValidKey_ValFound_NoOverwrite_ReturnFalse()
        {
            string key = "key";
            string value = "value";
            string value2 = "value2";
            ICache<string, string> cache = TestBasicCacheFactory.CreateLoadedCache(key, value);

            string expected = value;
            bool actual = cache.Put(key, value2, pOverwrite: false);
            string? actVal = cache.Get(key);

            actual.Should().BeFalse();
            actVal.Should().Be(expected);
        }

        [Fact]
        public void Put_InvalidKey_ReturnFalse()
        {
            string key = "key";
            string? key2 = null;
            string value = "value";
            ICache<string?, string> cache = TestBasicCacheFactory.CreateLoadedCache<string?, string>(key, value);

            string expected = value;
            bool actual = cache.Put(key2, value);
            string? actVal = cache.Get(key);

            actual.Should().BeFalse();
            actVal.Should().Be(expected);
        }
        #endregion
    }
}
