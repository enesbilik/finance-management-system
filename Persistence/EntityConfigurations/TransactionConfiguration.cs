using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions").HasKey(a => a.Id);

        builder.Property(a => a.Amount)
            .HasColumnName("Amount").IsRequired();

        builder.Property(a => a.Category)
            .HasColumnName("Category").IsRequired();

        builder.Property(a => a.Description)
            .HasColumnName("Description");
    }
}