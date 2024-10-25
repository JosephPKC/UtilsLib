﻿using LiteDB;
using Moq;

using LogWrapper.Loggers;
using LogWrapper.Loggers.Null;

using LiteDbWrapper.Test.Fakes;
using LiteDbWrapper.Wrappers;

namespace LiteDbWrapper.Test.Wrappers
{
    public class SimpleLiteDbWrapperTest
	{
		#region "Test Values"
		private int TestId1 { get; } = 0;
		private string TestValue1 { get; } = "test";

		private int TestId2 { get; } = 1;
		private string TestValue2 { get; } = "test2";

		#endregion
		private TestLiteDbModel CreateTestLiteDbModel()
		{
			return new()
			{
				Id = TestId1,
				TestValue = TestValue1
			};
		}

		private IEnumerable<TestLiteDbModel> CreateTestLiteDbModels()
		{
			return [CreateTestLiteDbModel(), new() { Id = TestId2, TestValue = TestValue2 }];
		}

		private Mock<ILiteDatabase> CreateTestLiteDatabaseMock(ILiteCollection<TestLiteDbModel> pLiteCollection)
		{
			Mock<ILiteDatabase> mock = new();
			_ = mock.Setup(x => x.GetCollection<TestLiteDbModel>(It.IsAny<string>(), It.IsAny<BsonAutoId>())).Returns(pLiteCollection);
			return mock;
		}

		private Mock<ILiteDatabase> CreateTestLiteDatabaseNonGenericMock(ILiteCollection<BsonDocument> pLiteCollection)
		{
			Mock<ILiteDatabase> mock = new();
			_ = mock.Setup(x => x.GetCollection(It.IsAny<string>(), It.IsAny<BsonAutoId>())).Returns(pLiteCollection);
			return mock;
		}

		private ILiteDbWrapper CreateSimpleLiteDbWrapper(ILiteDatabase pDb)
		{
			ILogger logger = new NullLoggerFactory().CreateNewLogger(typeof(SimpleLiteDbWrapperTest));
			return new LiteDbWrapperFactory().CreateNewWrapper(pDb, logger);
		}

		private ILiteDbWrapper CreateSimpleLiteDbWrapper(ILiteCollection<TestLiteDbModel> pLiteCollection)
		{
			Mock<ILiteDatabase> mock = CreateTestLiteDatabaseMock(pLiteCollection);
			return CreateSimpleLiteDbWrapper(mock.Object);
		}

		private ILiteDbWrapper CreateSimpleLiteDbWrapper(ILiteCollection<BsonDocument> pLiteCollection)
		{
			Mock<ILiteDatabase> mock = CreateTestLiteDatabaseNonGenericMock(pLiteCollection);
			return CreateSimpleLiteDbWrapper(mock.Object);
		}

		#region "GetAll"
		[Fact]
		public void Test_GetAll_CollectionHasData_ReturnCollectionOfModels()
		{
			// Arrange
			IEnumerable<TestLiteDbModel> testModels = [CreateTestLiteDbModel()];

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			mockCollection.Setup(x => x.FindAll()).Returns(testModels);

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			ICollection<TestLiteDbModel> actual = wrapper.GetAll<TestLiteDbModel>(colName);
			int expectedCount = 1;
			int expectedTestId = TestId1;
			string expectedTestValue = TestValue1;

			// Assert
			Assert.Equal(expectedCount, actual.Count);
			Assert.Equal(expectedTestId, actual.First().Id);
			Assert.Equal(expectedTestValue, actual.First().TestValue);
		}

		[Fact]
		public void Test_GetAll_CollectionIsEmpty_ReturnEmptyCollection()
		{
			// Arrange
			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			_ = mockCollection.Setup(x => x.FindAll()).Returns([]);

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			ICollection<TestLiteDbModel> actual = wrapper.GetAll<TestLiteDbModel>(colName);
			int expectedCount = 0;

			// Assert
			Assert.Equal(expectedCount, actual.Count);
		}
		#endregion

		#region "GetById"
		[Fact]
		public void Test_GetById_IdExistsInCollection_ReturnModel()
		{
			// Arrange
			TestLiteDbModel testModel = CreateTestLiteDbModel();

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			_ = mockCollection.Setup(x => x.FindById(It.IsAny<BsonValue>())).Returns(testModel);

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";
			int id = TestId1;

			// Act
			TestLiteDbModel? actual = wrapper.GetById<TestLiteDbModel>(colName, id);
			int expectedTestId = TestId1;
			string expectedTestValue = TestValue1;

			// Assert
			Assert.NotNull(actual);
			Assert.Equal(expectedTestId, actual.Id);
			Assert.Equal(expectedTestValue, actual.TestValue);
		}

		[Fact]
		public void Test_GetById_IdDoesNotExistInCollection_ReturnNull()
		{
			// Arrange
			TestLiteDbModel? testModel = null;

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
#pragma warning disable CS8604 // Possible null reference argument.
			_ = mockCollection.Setup(x => x.FindById(It.IsAny<BsonValue>())).Returns(testModel);
#pragma warning restore CS8604 // Possible null reference argument.

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";
			int id = TestId1;

			// Act
			TestLiteDbModel? actual = wrapper.GetById<TestLiteDbModel>(colName, id);

			// Assert
			Assert.Null(actual);
		}
		#endregion

		#region "Add"
		[Fact]
		public void Test_Add_AddModel()
		{
			// Arrange
			TestLiteDbModel testModel = CreateTestLiteDbModel();

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			mockCollection.Setup(x => x.Insert(testModel.Id, testModel)).Verifiable();

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			wrapper.Add(colName, testModel);

			// Assert
			mockCollection.Verify(x => x.Insert(testModel.Id, testModel), Times.Once);
		}
		#endregion

		#region "AddAll"
		[Fact]
		public void Test_AddAll_AddModels()
		{
			// Arrange
			IEnumerable<TestLiteDbModel> testModels = CreateTestLiteDbModels();

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			mockCollection.Setup(x => x.Insert(It.IsAny<BsonValue>(), It.IsAny<TestLiteDbModel>())).Verifiable();

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			wrapper.AddAll(colName, (ICollection<TestLiteDbModel>)testModels);

			// Assert
			mockCollection.Verify(x => x.Insert(It.IsAny<BsonValue>(), It.IsAny<TestLiteDbModel>()), Times.Exactly(2));
		}

		[Fact]
		public void Test_AddAll_EmptyCollection_DontAddModels()
		{
			// Arrange
			IEnumerable<TestLiteDbModel> testModels = [];

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			mockCollection.Setup(x => x.Insert(It.IsAny<BsonValue>(), It.IsAny<TestLiteDbModel>())).Verifiable();

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			wrapper.AddAll(colName, (ICollection<TestLiteDbModel>)testModels);

			// Assert
			mockCollection.Verify(x => x.Insert(It.IsAny<BsonValue>(), It.IsAny<TestLiteDbModel>()), Times.Never);
		}
		#endregion

		#region "UpdateById"
		[Fact]
		public void Test_UpdateById_UpdateModel()
		{
			// Arrange
			TestLiteDbModel testModel = CreateTestLiteDbModel();

			Mock<ILiteCollection<TestLiteDbModel>> mockCollection = new();
			mockCollection.Setup(x => x.Update(testModel.Id, testModel)).Verifiable();

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			wrapper.UpdateById(colName, testModel.Id, testModel);

			// Assert
			mockCollection.Verify(x => x.Update(testModel.Id, testModel), Times.Once);
		}
		#endregion

		#region "Drop"
		[Fact]
		public void Test_Drop_DropCollection()
		{
			// Arrange
			string colName = "test";

			Mock<ILiteDatabase> mock = new();
			_ = mock.Setup(x => x.DropCollection(colName)).Returns(true);

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mock.Object);

			// Act
			wrapper.Drop(colName);

			// Assert
			mock.Verify(x => x.DropCollection(colName), Times.Once);
		}
		#endregion

		#region "DeleteById"
		[Fact]
		public void Test_DeleteById_DeleteModel()
		{
			// Arrange
			int testIdToDelete = TestId1;
			BsonValue testBsonValueToDelete = new(testIdToDelete);
			Mock<ILiteCollection<BsonDocument>> mockCollection = new();
			_ = mockCollection.Setup(x => x.Delete(testBsonValueToDelete)).Returns(true);

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			wrapper.DeleteById(colName, testIdToDelete);

			// Assert
			mockCollection.Verify(x => x.Delete(testBsonValueToDelete), Times.Once);
		}
		#endregion

		#region "DeleteAll"
		[Fact]
		public void Test_DeleteAll_DeleteAllModels()
		{
			// Arrange
			int testDeleteAllReturn = 0;
			Mock<ILiteCollection<BsonDocument>> mockCollection = new();
			_ = mockCollection.Setup(x => x.DeleteAll()).Returns(testDeleteAllReturn);

			ILiteDbWrapper wrapper = CreateSimpleLiteDbWrapper(mockCollection.Object);
			string colName = "test";

			// Act
			wrapper.DeleteAll(colName);

			// Assert
			mockCollection.Verify(x => x.DeleteAll(), Times.Once);
		}
		#endregion
	}
}
