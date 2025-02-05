using Dima.Core.Handlers;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;

namespace Dima.Api.Common.Api;

public interface IEndPoint
{
    public abstract static void Map(IEndpointRouteBuilder app);

}
