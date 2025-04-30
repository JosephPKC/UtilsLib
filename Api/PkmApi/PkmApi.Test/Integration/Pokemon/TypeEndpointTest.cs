using FluentAssertions;
using PkmApi.Dtos.Pokemon.Type;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Pokemon
{
    public class TypeEndpointTest
    {
        [Fact]
        public void Type_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Type.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Type_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            TypeDto? actual = api.Type.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
