using AutoMapper;
using MediatR;
using Phonebook.Application.DTOs.UserDto;
using Phonebook.Application.Identity.CheckToken;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Users.Queries.GetAllUser
{
    public class GetAllUserCommandHandler : IRequestHandler<GetAllUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _user;
        private readonly IChekSign_ExpToken _check;
        private readonly IMapper _mapper;

        public GetAllUserCommandHandler(IUserRepository user,IChekSign_ExpToken Check,IMapper mapper)
        {
            _user = user;
            _check = Check;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            var ResultCheckToken=await _check.Check();
            if(!ResultCheckToken.Success) return ResultCheckToken;

            var response=new BaseCommandResponse();

            var isAdmin=await _user.IsAdmin(ResultCheckToken.Id.GetValueOrDefault());
            if (!isAdmin)
            {
                response.Status = 403;
                response.Success = false;
                response.Message = "شما به این بخش دسترسی ندارید.";
                return response;
            }

            var GetAllUser=await _user.GetAll();
            var GetAllUserDtos=_mapper.Map<List<GetUserDto>>(GetAllUser);
            response.Status=200;
            response.Success=true;
            response.Message = "عملیات موفقیت آمیز بود";
            response.Body= GetAllUserDtos;
            return response;
        }
    }
}
