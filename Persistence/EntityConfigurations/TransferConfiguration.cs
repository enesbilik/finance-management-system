using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("Transfers").HasKey(a => a.Id);

        builder.Property(a => a.Amount)
            .HasColumnName("Amount").IsRequired();

        builder.Property(a => a.TransferStatus)
            .HasColumnName("TransferStatus").IsRequired();
    }
}