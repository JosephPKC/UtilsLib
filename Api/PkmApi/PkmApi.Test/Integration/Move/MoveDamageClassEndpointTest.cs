using FluentAssertions;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Move
{
    public class MoveDamageClassEndpointTest
    {
        [Fact]
        public void MoveDamageClass_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.MoveDamageClass.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void MoveDamageClass_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            MoveDamageClassDto? actual = api.MoveDamageClass.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
