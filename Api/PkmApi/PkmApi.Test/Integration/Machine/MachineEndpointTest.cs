using FluentAssertions;
using PkmApi.Dtos.Machine.Machine;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Machine
{
    public class MachineEndpointTest
    {
        [Fact]
        public void Machine_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Machine.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Machine_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            MachineDto? actual = api.Machine.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
