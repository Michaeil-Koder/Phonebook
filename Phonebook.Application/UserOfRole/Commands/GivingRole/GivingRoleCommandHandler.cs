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

namespace Phonebook.Application.UserOfRole.Commands.GivingRole
{
    public class GivingRoleCommandHandler : IRequestHandler<GivingRoleCommand, BaseCommandResponse>
    {
        private readonly IUserRoleRepository _userRole;
        private readonly IUserRepository _user;
        private readonly IChekSign_ExpToken _check;
        private readonly IValidator<GivingRoleCommand> _validator;
        private readonly IMapper _mapper;

        public GivingRoleCommandHandler(IUserRoleRepository userRole,IUserRepository user,IChekSign_ExpToken Check,IValidator<GivingRoleCommand> validator , IMapper mapper)
        {
            _userRole = userRole;
            _user = user;
            _check = Check;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(GivingRoleCommand request, CancellationToken cancellationToken)
        {
            #region Check Token For Auth
            var resultCheckToken = _check.Check();
            if (!resultCheckToken.Success)
            {
                return resultCheckToken;
            }
            #endregion

            var response = new BaseCommandResponse();

            #region Checking the user for the primary role 
            var CountUser = await _user.GetCount();
            if (CountUser != 1)
            {
                var FindUser = await _user.Get(resultCheckToken.Id.GetValueOrDefault());
                if (FindUser == null)
                {
                    response.Message = "کاربری یافت نشد";
                    response.Success = false;
                    response.Status = 401;
                    return response;
                }
                var isAdmin = await _userRole.IsAdmin(FindUser.Id);
                if (!isAdmin)
                {
                    response.Message = "شما به این بخش دسترسی ندارید.";
                    response.Success = false;
                    response.Status = 403;
                    return response;
                }

            }
            #endregion


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

            var newUserRole=_mapper.Map<UserRole>(request);

             await _userRole.Create(newUserRole);
            response.Status = 201;
            response.Success = true;
            response.Message = "نقش با موفقیت داده شد";
            return response;
        }
    }
}
