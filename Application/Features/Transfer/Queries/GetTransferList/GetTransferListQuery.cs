using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Transfer.Queries.GetTransferList;

public class GetTransferListQuery : IRequest<List<GetTransferListItemDto>>
{
    public class GetTransferListQueryHandler : IRequestHandler<GetTransferListQuery,
        List<GetTransferListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransferRepository _transferRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTransferListQueryHandler(IMapper mapper,
            ITransferRepository transferRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _transferRepository = transferRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<GetTransferListItemDto>> Handle(
            GetTransferListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId =
                _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var transfers = await _transferRepository.GetListAsync(
                t => (t.FromUserId.ToString() == currentUserId ||
                      t.ToUserId.ToString() == currentUserId) && !t.IsDeleted,
                cancellationToken: cancellationToken);

            List<GetTransferListItemDto> response =
                _mapper.Map<List<GetTransferListItemDto>>(transfers);

            return response;
        }
    }
}