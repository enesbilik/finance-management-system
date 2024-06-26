using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transaction.Commands.Delete;

public class DeleteTransactionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>,
        ICacheRemoverRequest
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;

        public DeleteTransactionCommandHandler(IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _transactionRepository = transactionRepository;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request,
            CancellationToken cancellationToken)
        {
            // user just can delete their own transactions

            var transaction = await _transactionRepository.GetAsync(t => t.Id == request.Id,
                cancellationToken: cancellationToken);

            if (transaction == null)
            {
                throw new BusinessException("Transaction not found");
            }

            await _transactionRepository.DeleteAsync(transaction);

            return Unit.Value;
        }

        public string CacheKey => $"GetTransactionList";
        public bool BypassCache { get; }
        public string? CacheGroupKey => "GetTransactions";
    }
}