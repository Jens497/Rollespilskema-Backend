using Microsoft.EntityFrameworkCore;

namespace RoleplayingSchemaBackend.Data
{
    public class RoleplayingDbContext : DbContext
    {

        public RoleplayingDbContext(DbContextOptions<RoleplayingDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
    }
}
