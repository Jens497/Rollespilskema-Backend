using MediatR;

namespace RoleplayingSchemaBackend.Handlers.Interface
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    {
        //Do specific things for the Commands
    }

}
