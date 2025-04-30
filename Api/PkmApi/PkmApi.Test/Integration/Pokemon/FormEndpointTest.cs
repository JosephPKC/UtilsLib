using FluentAssertions;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Utility;

namespace PkmApi.Test.Integration.Pokemon
{
    public class FormEndpointTest
    {
        [Fact]
        public void Form_GetAll_ReturnAllResultList()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();

            ResLiDto? actual = api.Form.GetAll();

            actual.Should().NotBeNull();
            actual.Results.Should().NotBeEmpty();
        }

        [Fact]
        public void Form_GetById_ReturnResult()
        {
            IPkmApi api = PkmApiFactory.CreatePkmApi();
            string id = "1";

            int expId = 1;
            FormDto? actual = api.Form.GetById(id);

            actual.Should().NotBeNull();
            actual.Id.Should().Be(expId);
        }
    }
}
