using Common.ErrorHandling;
using MediatR;

namespace Common.CQRS.Command;

public interface ICommandHandler<in TCommand> 
    : IRequestHandler<TCommand, ErrorOr<Unit>> where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>;