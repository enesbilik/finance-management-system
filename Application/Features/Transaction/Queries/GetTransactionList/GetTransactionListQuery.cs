using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transaction.Queries.GetTransactionList;

public class GetTransactionListQuery : IRequest<List<GetTransactionListItemDto>>, ILoggableRequest,
    ICacheableRequest
{
    public class GetTransactionListQueryHandler : IRequestHandler<GetTransactionListQuery,
        List<GetTransactionListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTransactionListQueryHandler(IMapper mapper,
            ITransactionRepository transactionRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<GetTransactionListItemDto>> Handle(
            GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId =
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var transactions = await _transactionRepository.GetListAsync(
                t => t.AppUserId.ToString() == currentUserId && t.IsDeleted != true,
                cancellationToken: cancellationToken);

            List<GetTransactionListItemDto> response =
                _mapper.Map<List<GetTransactionListItemDto>>(transactions);

            return response;
        }
    }

    public string CacheKey => $"GetTransactionList";
    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }
    public string? CacheGroupKey => "GetTransactions";
}