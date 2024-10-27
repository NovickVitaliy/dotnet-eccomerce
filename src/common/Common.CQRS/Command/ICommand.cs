using Common.ErrorHandling;
using MediatR;

namespace Common.CQRS.Command;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest<ErrorOr<Unit>>;