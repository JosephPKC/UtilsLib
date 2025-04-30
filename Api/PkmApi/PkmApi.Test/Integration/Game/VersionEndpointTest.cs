using FluentAssertions;
using PkmApi.Dtos.Game.Version;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Game
{
    public class VersionEndpointTest
    {
        [Fact]
        public void Version_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Version.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Version_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            VersionDto? actual = api.Version.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
