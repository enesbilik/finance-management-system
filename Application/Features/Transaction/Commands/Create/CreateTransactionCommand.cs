using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transaction.Commands.Create;

public class CreateTransactionCommand : IRequest<CreatedTransactionResponse>, ILoggableRequest,
    ICacheRemoverRequest
{
    public Guid AppUserId { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }

    public class
        CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand,
            CreatedTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(IMapper mapper,
            IHttpContextAccessor httpContextAccessor, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _transactionRepository = transactionRepository;
        }

        public async Task<CreatedTransactionResponse> Handle(CreateTransactionCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId =
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != request.AppUserId.ToString())
            {
                throw new UnauthorizedAccessException();
            }

            if (request.Amount <= 0)
            {
                throw new BusinessException("Amount must be greater than 0");
            }

            var transaction = _mapper.Map<Domain.Entities.Transaction>(request);

            await _transactionRepository.AddAsync(transaction);

            var createdTransactionResponse = _mapper.Map<CreatedTransactionResponse>(transaction);

            return createdTransactionResponse;
        }
    }

    public string CacheKey => $"GetTransactionList";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "GetTransactions";
}