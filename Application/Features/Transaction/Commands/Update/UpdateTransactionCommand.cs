using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transaction.Commands.Update;

public class UpdateTransactionCommand : IRequest<UpdatedTransactionResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }

    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand,
        UpdatedTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;

        public UpdateTransactionCommandHandler(IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _transactionRepository = transactionRepository;
        }


        public async Task<UpdatedTransactionResponse> Handle(UpdateTransactionCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
            {
                throw new BusinessException("Amount must be greater than 0");
            }

            var transaction = await _transactionRepository.GetAsync(
                t => t.Id == request.Id,
                cancellationToken: cancellationToken);

            transaction = _mapper.Map(request, transaction);

            await _transactionRepository.UpdateAsync(transaction!);

            var updatedTransactionResponse = _mapper.Map<UpdatedTransactionResponse>(transaction);

            return updatedTransactionResponse;
        }
    }

    public string CacheKey => $"GetTransactionList";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetTransactions";
}