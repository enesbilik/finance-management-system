using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TransferRepository : EfRepositoryBase<Transfer, Guid, BaseDbContext>,
    ITransferRepository
{
    public TransferRepository(BaseDbContext context) : base(context)
    {
    }
}