using MediatR;
using Shared.Models.Models;

namespace Shared.Core.Common.Messaging
{
    public interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery , Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
