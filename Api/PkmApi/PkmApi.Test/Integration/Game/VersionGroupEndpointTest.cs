using FluentAssertions;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Game
{
    public class VersionGroupEndpointTest
    {
        [Fact]
        public void VersionGroup_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.VersionGroup.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void VersionGroup_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            VersionGroupDto? actual = api.VersionGroup.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
