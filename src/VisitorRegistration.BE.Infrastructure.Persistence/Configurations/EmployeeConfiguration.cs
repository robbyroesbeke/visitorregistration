// (c) Visitor Registration

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure (EntityTypeBuilder<Employee> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.HasKey(employee => employee.Id)
            .HasName("PK_EmployeeID");

        _ = builder.Property(employee => employee.FirstName)
            .IsRequired()
            .HasMaxLength(20);

        _ = builder.Property(employee => employee.LastName)
            .IsRequired()
            .HasMaxLength(30);

        _ = builder.Property(employee => employee.EmailAddress)
            .IsRequired()
            .HasMaxLength(75);
    }
}