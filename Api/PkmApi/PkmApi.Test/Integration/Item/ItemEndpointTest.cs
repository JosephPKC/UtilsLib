using FluentAssertions;
using PkmApi.Dtos.Item.Item;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Item
{
    public class ItemEndpointTest
    {
        [Fact]
        public void Item_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Item.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Item_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            ItemDto? actual = api.Item.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
