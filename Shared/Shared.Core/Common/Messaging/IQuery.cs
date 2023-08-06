using MediatR;
using Shared.Models.Models;

namespace Shared.Core.Common.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
