using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Common;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Config.RoleConfig;

namespace Phonebook.Infrastructure.AppDbContext
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }

        public DbSet<User> Users {  get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<UserNumbers> UserNumbers { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserRoleConfig).Assembly);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess ,CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdatedAt = DateTime.Now;
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess ,cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdatedAt = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
