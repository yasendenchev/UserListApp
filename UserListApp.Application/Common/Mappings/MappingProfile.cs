﻿using AutoMapper;
using UserListApp.Application.DTO;
using UserListApp.Domain.Entities;

namespace UserListApp.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}
