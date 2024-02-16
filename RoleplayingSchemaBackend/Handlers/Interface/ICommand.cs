using MediatR;

namespace RoleplayingSchemaBackend.Handlers.Interface
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
}
