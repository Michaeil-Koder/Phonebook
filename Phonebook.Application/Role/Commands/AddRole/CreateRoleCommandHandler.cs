using AutoMapper;
using FluentValidation;
using MediatR;
using Phonebook.Application.Identity.CheckToken;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Role.Commands.AddRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, BaseCommandResponse>
    {
        private readonly IRoleRepository _role;
        private readonly IChekSign_ExpToken _check;
        private readonly IValidator<CreateRoleCommand> _validator;
        private readonly IUserRepository _user;
        private readonly IUserRoleRepository _userRole;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleRepository role,IChekSign_ExpToken Check,IValidator<CreateRoleCommand> validator ,IUserRepository user,IUserRoleRepository userRole,IMapper mapper)
        {
            _role = role;
            _check = Check;
            _validator = validator;
            _user = user;
            _userRole = userRole;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var resultCheckToken=_check.Check();
            if (!resultCheckToken.Success)
            {
                return resultCheckToken;
            }

            var response = new BaseCommandResponse();

            var ValidatorResult=await _validator.ValidateAsync(request, cancellationToken);
            if(!ValidatorResult.IsValid)
            {
                response.Status = 400;
                response.Success = false;
                response.Message = "خطایی رخ داده است.";
                response.Errors=ValidatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }

            var CountUser=await _user.GetCount();

            if (CountUser!=1)
            {
                var FindUser = await _user.Get(resultCheckToken.Id.GetValueOrDefault());
                if (FindUser==null)
                {
                    response.Message = "کاربری یافت نشد";
                    response.Success=false;
                    response.Status = 401;
                    return response;
                }
                var isAdmin=await _userRole.IsAdmin(FindUser.Id);
                if (!isAdmin)
                {
                    response.Message = "شما به این بخش دسترسی ندارید.";
                    response.Success = false;
                    response.Status = 403;
                    return response;
                }

            }

            var roleEntity = _mapper.Map<Roles>(request);
            var newRole=await _role.Create(roleEntity);
            if (newRole!=null)
            {
                response.Success = true;
                response.Status = 201;
                response.Message = "با موفقیت ایجاد شد";
                response.Id= newRole.Id;
                return response;
            }

            response.Success = false;
            response.Message = "خطایی در ایجائ کردن رخ داده است.";
            return response;
        }
    }
}
