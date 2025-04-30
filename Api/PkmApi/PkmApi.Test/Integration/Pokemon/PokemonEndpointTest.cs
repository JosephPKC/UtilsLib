using FluentAssertions;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Pokemon
{
    public class PokemonEndpointTest
    {
        [Fact]
        public void Pokemon_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Pokemon.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Pokemon_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            PkmDto? actual = api.Pokemon.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
