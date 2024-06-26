using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transaction.Queries.GetById;

public class GetByIdTransactionQuery : IRequest<GetByIdTransactionResponse>
{
    public Guid Id { get; set; }


    public class
        GetByIdTransactionQueryHandler : IRequestHandler<GetByIdTransactionQuery,
            GetByIdTransactionResponse>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetByIdTransactionQueryHandler(ITransactionRepository transactionRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<GetByIdTransactionResponse> Handle(GetByIdTransactionQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId =
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var transaction = await _transactionRepository.GetAsync(
                predicate: t =>
                    t.Id == request.Id && t.AppUserId.ToString() == currentUserId && !t.IsDeleted,
                cancellationToken: cancellationToken
            );

            var response = _mapper.Map<GetByIdTransactionResponse>(transaction);
            return response;
        }
    }
}