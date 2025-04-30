using FluentAssertions;

using LogWrapper.Loggers.Null;

using PkmApi.Dtos.Utility;
using PkmApi.Endpoints;

using PkmApi.Test.Fakes;

namespace PkmApi.Test.Endpoints
{
    public class BasePkmEndpointHandlerTest
    {
        #region Setup
        private static IEndpointHandler<BasicTestDto> GetEndpointHandler(Type pDTOTypeToTest)
        {
            NullGetter getter = new()
            {
                DTOJsonType = pDTOTypeToTest
            };
            return EndpointHandlerFactory.BuildEndpointHandler<BasicTestDto>("test-uri/api", "v1", "test-endpoint", getter, new BasicParser(), new NullLoggerFactory().CreateNewLogger(typeof(IEndpointHandler<BasicTestDto>)));
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_ReturnDeserializedJsonDTO()
        {
            IEndpointHandler<BasicTestDto> handler = GetEndpointHandler(typeof(BasicTestDto));
            string id = "1";

            BasicTestDto expected = TestValues.Test_BasicTestDto;
            BasicTestDto? actual = handler.GetById(id);

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }
        #endregion

        #region GetAll
        [Fact]
        public void GetAll_ReturnDeserializedJsonResLiDTO()
        {
            IEndpointHandler<BasicTestDto> handler = GetEndpointHandler(typeof(ResLiDto));

            ResLiDto expected = TestValues.Test_ResLiDto;
            ResLiDto? actual = handler.GetAll();

            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }
        #endregion
    }
}
