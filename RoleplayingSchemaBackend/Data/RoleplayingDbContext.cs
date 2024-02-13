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
        public DbSet<Template> Templates { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Component> Components { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Component>().ToTable(nameof(Component));
            builder.Entity<Template>().ToTable(nameof(Template));
            builder.Entity<Sheet>().ToTable(nameof(Sheet));
            this.SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin"},
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User"}
                );
        }
    }
}
