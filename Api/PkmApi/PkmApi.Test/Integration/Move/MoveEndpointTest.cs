using FluentAssertions;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Move
{
    public class MoveEndpointTest
    {
        [Fact]
        public void Move_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Move.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Move_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            MoveDto? actual = api.Move.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
