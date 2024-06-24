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

namespace Phonebook.Application.Role.Commands.EditRole
{
    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, BaseCommandResponse>
    {
        private readonly IValidator<EditRoleCommand> _validator;
        private readonly IChekSign_ExpToken _check;
        private readonly IUserRoleRepository _userRole;
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _role;

        public EditRoleCommandHandler(IValidator<EditRoleCommand> validator,IChekSign_ExpToken check,IUserRoleRepository userRole,IUserRepository user , IMapper mapper,IRoleRepository role)
        {
            _validator = validator;
            _check = check;
            _userRole = userRole;
            _user = user;
            _mapper = mapper;
            _role = role;
        }

        public async Task<BaseCommandResponse> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            #region Check Token For Auth
            var resultCheckToken = await _check.Check();
            if (!resultCheckToken.Success)
            {
                return resultCheckToken;
            }
            #endregion

            var response = new BaseCommandResponse();

            #region Validator For Check Request
            var ValidatorResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!ValidatorResult.IsValid)
            {
                response.Status = 400;
                response.Success = false;
                response.Message = "خطایی رخ داده است.";
                response.Errors = ValidatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }
            #endregion

            var ExistUser=await _user.Exist(resultCheckToken.Id.GetValueOrDefault());
            var isAdmin=await _userRole.IsAdmin(resultCheckToken.Id.GetValueOrDefault());
            if(!ExistUser ||!isAdmin)
            {
                response.Status = 403;
                response.Success=false;
                response.Message = "شما دسترسی به این بخش ندارید.";
                return response;
            }

            var ExistRole=await _role.Exist(request.RoleId);
            if(!ExistRole)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = "این نقش یافت نشد.";
                return response;
            }

            var OldRole=await _role.Get(request.RoleId);

            OldRole.Role=request.RoleName;
            var resultUpdate=await _role.Update(OldRole);
            if (!resultUpdate)
            {
                response.Status = 400;
                response.Success = false;
                response.Message = "به دلیل مشکلاتی آپدیت رخ نداد .";
                return response;
            }

            response.Success=true;
            response.Message = "با موفقیت آپدیت شد";
            return response;
        }
    }
}
