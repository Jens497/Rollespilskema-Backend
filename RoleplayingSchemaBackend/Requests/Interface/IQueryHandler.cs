using MediatR;

namespace RoleplayingSchemaBackend.Requests.Interface
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        //Do specific things for the Queries
    }
}
