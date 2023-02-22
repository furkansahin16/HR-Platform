using Core.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EfContext.Configurations
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Personnel).WithMany(x => x.Permissons).HasForeignKey(x => x.PersonnelId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
