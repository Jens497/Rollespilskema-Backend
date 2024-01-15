using MediatR;

namespace RoleplayingSchemaBackend.Requests.Interface
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {

    }
}
