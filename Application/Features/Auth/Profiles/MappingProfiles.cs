using System;
using AutoMapper;
using Domain.Entities;
using Application.Features.Auth.Commands.Register;

namespace Application.Features.Auth.Profiles;


public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUser, RegisterCommand>().ReverseMap();
        CreateMap<AppUser, RegisteredCommandResponse>().ReverseMap();
        
    }
}

