using Core.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser<Guid>, IEntityTimestamps
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public decimal Balance { get; set; } = 1000;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }

    public AppUser()
    {
        Transactions = new List<Transaction>();
    }
}