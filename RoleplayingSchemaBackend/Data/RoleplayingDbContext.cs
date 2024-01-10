using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RoleplayingSchemaBackend.Data
{
    public class RoleplayingDbContext : IdentityDbContext<Users>
    {

        public RoleplayingDbContext(DbContextOptions<RoleplayingDbContext> options) : base(options)
        {

        }
        //public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
