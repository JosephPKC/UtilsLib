using FluentAssertions;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Game
{
    public class PokedexEndpointTest
    {
        [Fact]
        public void Pokedex_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Pokedex.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Pokedex_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            PokedexDto? actual = api.Pokedex.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
