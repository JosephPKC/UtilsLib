using SqliteDbWrapper.Cache;

namespace SQLiteDbWrapper.Test.CacheTests
{
	/// <summary>
	/// Tests the SimpleSqliteDbCache implementation.
	/// </summary>
	public class SimpleCacheTests
	{
		/// <summary>
		/// Creates a test version of the SimpleSqliteDbCache with generic type string that is empty.
		/// </summary>
		/// <param name="pKey"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		private ISqliteDbCache<string> CreateEmptyTestSimpleStringCache()
		{
			ISqliteDbCache<string> cache = new SimpleSqliteDbCacheFactory().CreateNewCache<string>();

			return cache;
		}

		/// <summary>
		/// Creates a test version of the SimpleSqliteDbCache with generic type string, loaded with pKey and pValue.
		/// </summary>
		/// <param name="pKey"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		private ISqliteDbCache<string> CreateLoadedTestSimpleStringCache(string pKey, string pValue)
		{
			ISqliteDbCache<string> cache = new SimpleSqliteDbCacheFactory().CreateNewCache<string>();
			int lifeInS = 300;
			cache.Store(pKey, pValue, lifeInS);

			return cache;
		}

		#region "Retrieve"
		[Fact]
		public void Test_Retrieve_KeyExists_ReturnItem()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);

			// Act
			string? actual = cache.Retrieve(key);
			string expected = value;

			// Assert
			Assert.NotNull(actual);
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void Test_Retrieve_KeyDoesNotExist_ReturnNull()
		{
			// Arrange
			ISqliteDbCache<string> cache = CreateEmptyTestSimpleStringCache();
			string key = "key";

			// Act
			string? actual = cache.Retrieve(key);

			// Assert
			Assert.Null(actual);
		}

		[Fact]
		public void Test_Retrieve_KeyIsNullOrWhitespace_ReturnNull()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);
			string key2 = "";

			// Act
			string? actual = cache.Retrieve(key2);

			// Assert
			Assert.Null(actual);
		}
		#endregion

		#region "Store"
		[Fact]
		public void Test_Store_KeyExists_ReturnFalse()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);
			string key2 = key;
			string value2 = "value2";
			int lifeInS = 0;

			// Act
			bool actual = cache.Store(key2, value2, lifeInS);

			// Assert
			Assert.False(actual);
		}

		[Fact]
		public void Test_Store_KeyDoesNotExist_ReturnTrue()
		{
			// Arrange
			ISqliteDbCache<string> cache = CreateEmptyTestSimpleStringCache();
			string key = "key";
			string value = "value";
			int lifeInS = 0;

			// Act
			bool actual = cache.Store(key, value, lifeInS);
			string? actualItem = cache.Retrieve(key);
			string expected = value;

			// Assert
			Assert.True(actual);
			Assert.NotNull(actualItem);
			Assert.Equal(expected, actualItem);
		}

		[Fact]
		public void Test_Store_KeyIsNullOrWhitespace_ReturnFalse()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);
			string key2 = "";
			string value2 = "value2";
			int lifeInS = 0;

			// Act
			bool actual = cache.Store(key2, value2, lifeInS);

			// Assert
			Assert.False(actual);
		}

		[Fact]
		public void Test_Store_ItemIsNullOrEmpty_ReturnTrue()
		{
			// Arrange
			ISqliteDbCache<string> cache = CreateEmptyTestSimpleStringCache();
			string key = "key";
			string value = string.Empty;
			int lifeInS = 0;

			// Act
			bool actual = cache.Store(key, value, lifeInS);

			// Assert
			Assert.True(actual);
		}
		#endregion

		#region "Update"
		[Fact]
		public void Test_Update_KeyExists_ReturnTrue()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);
			string key2 = key;
			string value2 = "value2";
			int lifeInS = 0;

			// Act
			bool actual = cache.Update(key2, value2, lifeInS);
			string? actualItem = cache.Retrieve(key);
			string expected = value2;

			// Assert
			Assert.True(actual);
			Assert.NotNull(actualItem);
			Assert.Equal(expected, actualItem);
		}

		[Fact]
		public void Test_Update_KeyDoesNotExist_ReturnTrue()
		{
			// Arrange
			ISqliteDbCache<string> cache = CreateEmptyTestSimpleStringCache();
			string key = "key";
			string value = "value";
			int lifeInS = 0;

			// Act
			bool actual = cache.Update(key, value, lifeInS);
			string? actualItem = cache.Retrieve(key);
			string expected = value;

			// Assert
			Assert.True(actual);
			Assert.NotNull(actualItem);
			Assert.Equal(expected, actualItem);
		}


		[Fact]
		public void Test_Update_KeyIsNullOrWhitespace_ReturnFalse()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);
			string key2 = "";
			string value2 = "value2";
			int lifeInS = 0;

			// Act
			bool actual = cache.Update(key2, value2, lifeInS);

			// Assert
			Assert.False(actual);
		}

		[Fact]
		public void Test_Update_ItemIsNullOrEmpty_ReturnTrue()
		{
			// Arrange
			ISqliteDbCache<string> cache = CreateEmptyTestSimpleStringCache();
			string key = "key";
			string value = string.Empty;
			int lifeInS = 0;

			// Act
			bool actual = cache.Update(key, value, lifeInS);

			// Assert
			Assert.True(actual);
		}
		#endregion

		#region "Flush"
		[Fact]
		public void Test_Flush_LoadedCache_EmptiedCache()
		{
			// Arrange
			string key = "key";
			string value = "value";
			ISqliteDbCache<string> cache = CreateLoadedTestSimpleStringCache(key, value);

			// Act
			cache.Flush();
			string? actualItem = cache.Retrieve(key);

			// Assert
			Assert.Null(actualItem);
		}
		#endregion
	}
}
