using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TransactionRepository : EfRepositoryBase<Transaction, Guid, BaseDbContext>,
    ITransactionRepository
{
    public TransactionRepository(BaseDbContext context) : base(context)
    {
    }
}