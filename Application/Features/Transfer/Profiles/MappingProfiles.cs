using Application.Features.Transaction.Commands.Create;
using Application.Features.Transfer.Commands.Create;
using Application.Features.Transfer.Queries.GetById;
using Application.Features.Transfer.Queries.GetTransferList;
using AutoMapper;

namespace Application.Features.Transfer.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Transfer, CreateTransferCommand>().ReverseMap();
        CreateMap<Domain.Entities.Transfer, CreatedTransferResponse>().ReverseMap();

        CreateMap<Domain.Entities.Transfer, GetByIdTransferResponse>().ReverseMap();
        
        CreateMap<Domain.Entities.Transfer, GetTransferListItemDto>().ReverseMap();
        CreateMap<List<Domain.Entities.Transfer>, GetTransferListItemDto>().ReverseMap();
    }
}