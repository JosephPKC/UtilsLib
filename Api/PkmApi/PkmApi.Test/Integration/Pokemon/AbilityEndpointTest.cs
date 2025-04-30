using FluentAssertions;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Pokemon
{
    public class AbilityEndpointTest
    {
        [Fact]
        public void Ability_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Ability.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Ability_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            AbilityDto? actual = api.Ability.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
