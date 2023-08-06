using MediatR;
using Shared.Models.Models;

namespace Shared.Core.Common.Messaging
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand,Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<Tcommand , TResponse> : IRequestHandler<Tcommand , Result<TResponse>> 
        where Tcommand : ICommand<TResponse>
    { }
}
