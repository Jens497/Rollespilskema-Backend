using MediatR;
using RoleplayingSchemaBackend.Commands;
using RoleplayingSchemaBackend.Data;

namespace RoleplayingSchemaBackend.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand>
    {
        private readonly RoleplayingDbContext _context;

        public AddUserHandler(RoleplayingDbContext context)
        {
            _context = context;
        }
        public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            _context.Users.Add(request.User);
            await _context.SaveChangesAsync();
        }
    }
}
