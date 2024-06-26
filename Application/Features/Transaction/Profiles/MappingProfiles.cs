using System;
using Application.Features.Transaction.Commands.Create;
using Application.Features.Transaction.Commands.Update;
using Application.Features.Transaction.Queries.GetById;
using Application.Features.Transaction.Queries.GetTransactionList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;

namespace Application.Features.Transaction.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Transaction, CreateTransactionCommand>().ReverseMap();
        CreateMap<Domain.Entities.Transaction, CreatedTransactionResponse>().ReverseMap();

        CreateMap<Domain.Entities.Transaction, UpdateTransactionCommand>().ReverseMap();
        CreateMap<Domain.Entities.Transaction, UpdatedTransactionResponse>().ReverseMap();


        CreateMap<Domain.Entities.Transaction, GetTransactionListItemDto>()
            .ReverseMap();

        CreateMap<List<Domain.Entities.Transaction>, GetTransactionListItemDto>()
            .ReverseMap();


        CreateMap<Paginate<Domain.Entities.Transaction>,
            GetListResponse<GetTransactionListItemDto>>().ReverseMap();

        CreateMap<Domain.Entities.Transaction, GetByIdTransactionResponse>().ReverseMap();
    }
}