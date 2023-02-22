using Core.Entites.Concrete;
using DataAccess.EfContext.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EfContext
{
    public class HRDbContext : IdentityDbContext<Employee>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer(@"
                Server=tcp:baonline2sqlserver.database.windows.net,1433;
                Initial Catalog=hrplatform-db;
                Persist Security Info=False;
                User ID=fsahin;
                Password=A123b456;
                MultipleActiveResultSets=False;
                Encrypt=True;
                TrustServerCertificate=False;
                Connection Timeout=30;"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeTypeConfiguration());
            modelBuilder.ApplyConfiguration<Permission>(new PermissionTypeConfiguration());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
