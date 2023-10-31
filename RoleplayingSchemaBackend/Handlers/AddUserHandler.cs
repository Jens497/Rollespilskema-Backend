using MediatR;
using RoleplayingSchemaBackend.Commands;

namespace RoleplayingSchemaBackend.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand>
    {
        private readonly UserData _userData;

        public AddUserHandler(UserData userData)
        {
            _userData = userData;
        }

        public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _userData.AddUser(request.User);
        }
    }
}
