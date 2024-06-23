﻿using AutoMapper;
using Phonebook.Application.Authentication.Commands.CreateUser;
using Phonebook.Application.DTOs;
using Phonebook.Application.Role.Commands.AddRole;
using Phonebook.Application.userNumber.Commands.CreateUserNumbers;
using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }
    }
}