using FluentAssertions;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Move
{
    public class MoveLearnMethodEndpointTest
    {
        [Fact]
        public void MoveLearnMethod_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.MoveLearnMethod.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void MoveLearnMethod_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            MoveLearnMethodDto? actual = api.MoveLearnMethod.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
