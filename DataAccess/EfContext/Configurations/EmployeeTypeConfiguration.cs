using Core.Entites.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EfContext.Configurations
{
    public class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(25);
            builder
                .Property(x => x.SecondName)
                .HasMaxLength(20);
            builder
                .Property(x => x.SecondLastName)
                .HasMaxLength(25);
            builder
                .Property(x => x.BirthDate)
                .IsRequired();
            builder
                .Property(x => x.Address)
                .HasMaxLength(200);
            builder
                .Property(x => x.StartDateOfWork)
                .IsRequired();
            
            builder
                .Property(x => x.DismissalDate);
            
            builder
                .Property(x => x.IsActive)
                .IsRequired();
            builder
                .Property(x => x.Job)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(x => x.Department)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(x => x.CompanyInfo)
                .HasMaxLength(200);
        }
    }
}
