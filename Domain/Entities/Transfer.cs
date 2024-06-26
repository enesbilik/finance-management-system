using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Transfer : Entity<Guid>
{
    public Guid FromUserId { get; set; }

    public Guid ToUserId { get; set; }
    public decimal Amount { get; set; }
    public TransferStatus TransferStatus { get; set; }


    public Transfer()
    {
        Id = Guid.NewGuid();
    }
}