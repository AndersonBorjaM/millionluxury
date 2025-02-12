using MediatR;
using Million.Domain.Abstractions;

namespace Million.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
