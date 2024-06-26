using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Transaction : Entity<Guid>
{
    public Guid AppUserId { get; set; }

    public AppUser AppUser { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }

    public Transaction()
    {
        Id = Guid.NewGuid();
    }
}