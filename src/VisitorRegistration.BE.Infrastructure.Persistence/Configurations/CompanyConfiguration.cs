// (c) Visitor Registration

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VisitorRegistration.BE.Core.Entities;

namespace VisitorRegistration.BE.Infrastructure.Persistence.EF.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure (EntityTypeBuilder<Company> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.HasKey(company => company.Id)
            .HasName("PK_CompanyID");

        _ = builder.Property(company => company.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}