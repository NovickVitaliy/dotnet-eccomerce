using MediatR;

namespace Common.CQRS.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>;