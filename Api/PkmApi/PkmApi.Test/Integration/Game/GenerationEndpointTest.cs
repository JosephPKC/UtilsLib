using FluentAssertions;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Game
{
    public class GenerationEndpointTest
    {
        [Fact]
        public void Generation_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Generation.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Generation_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            GenerationDto? actual = api.Generation.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
