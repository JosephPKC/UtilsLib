using FluentAssertions;

using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Pokemon
{
    public class SpeciesEndpointTest
    {
        [Fact]
        public void Species_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Species.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Species_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            SpeciesDto? actual = api.Species.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
