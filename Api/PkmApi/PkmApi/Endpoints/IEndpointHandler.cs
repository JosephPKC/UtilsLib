using PkmApi.Dtos;
using PkmApi.Dtos.Utility;

namespace PkmApi.Endpoints
{
    public interface IEndpointHandler<out TData> where TData : IPkmApiDto
    {
        ResLiDto? GetAll(int pLimit = 20, int pOffset = 0);
        TData? GetById(string pId);
    }
}
