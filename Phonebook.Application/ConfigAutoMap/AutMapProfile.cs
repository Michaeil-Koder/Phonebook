﻿using AutoMapper;
using Phonebook.Application.Authentication.Commands.CreateUser;
using Phonebook.Application.DTOs;
using Phonebook.Application.DTOs.Role;
using Phonebook.Application.DTOs.UserDto;
using Phonebook.Application.Role.Commands.AddRole;
using Phonebook.Application.Role.Commands.EditRole;
using Phonebook.Application.userNumber.Commands.CreateUserNumbers;
using Phonebook.Application.UserOfRole.Commands.GivingRole;
using Phonebook.Domain.Entities;

namespace Phonebook.Application.ConfigAutoMap
{
    public class AutMapProfile: Profile
    {
        public AutMapProfile() 
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<CreateUserNumbersCommand,UserNumbers>().ReverseMap();
            CreateMap<GenericUserNumbersDto,UserNumbers>().ReverseMap();
            CreateMap<CreateRoleCommand,Roles>().ReverseMap();
            CreateMap<EditRoleCommand, Roles>().ReverseMap();
            CreateMap<GetAllRoleDto, Roles>().ReverseMap();
            CreateMap<GivingRoleCommand, UserRole>().ReverseMap();
            CreateMap<GetUserDto,User>().ReverseMap();

        }
    }
}
