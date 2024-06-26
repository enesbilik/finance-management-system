using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transfer.Queries.GetById;

public class GetByIdTransferQuery : IRequest<GetByIdTransferResponse>
{
    public Guid Id { get; set; }


    public class
        GetByIdTransferQueryHandler : IRequestHandler<GetByIdTransferQuery,
            GetByIdTransferResponse>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetByIdTransferQueryHandler(ITransferRepository transferRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<GetByIdTransferResponse> Handle(GetByIdTransferQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId =
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var transfer = await _transferRepository.GetAsync(
                predicate: t =>
                    t.Id == request.Id && t.FromUserId.ToString() == currentUserId && !t.IsDeleted,
                cancellationToken: cancellationToken
            );

            var response = _mapper.Map<GetByIdTransferResponse>(transfer);
            return response;
        }
    }
}