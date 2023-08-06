using MediatR;
using Shared.Models.Models;

namespace Shared.Core.Common.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
