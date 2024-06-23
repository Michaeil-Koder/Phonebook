using FluentValidation;
using MediatR;
using Phonebook.Application.Identity.JsonWebToken;
using Phonebook.Application.Identity.PasswordHasher;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Authentication.Queries.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginUserCommand> _validator;
        private readonly IJsonWebToken _jsonWebToken;

        public LoginUserCommandHandler(IUserRepository userRepository,IValidator<LoginUserCommand> validator , IJsonWebToken jsonWebToken)
        {
            _userRepository = userRepository;
            _validator = validator;
            _jsonWebToken = jsonWebToken;
        }

        public async Task<BaseCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            #region validator
            var resultValidat = await _validator.ValidateAsync(request);
            if (!resultValidat.IsValid)
            {
                response.Success = false;
                response.Errors= resultValidat.Errors.Select(er=>er.ErrorMessage).ToList();
                response.Message = "کد ملی را چک کنید.";
                return response;
            }
            #endregion

            var user=await _userRepository.GetUserByNationalCode(request.NationalCode);
            var VerifyPas=PasswordHasher.VerifyPassword(request.Password, user.Password);
            if (!VerifyPas)
            {
                response.Success = false;
                response.Status = 403;
                response.Message = "پسوورد وارد شده اشتباه است.";
                return response;
            }

            var token = _jsonWebToken.GenerateToken(user.Id);

            response.Success = true;
            response.Message = "ورود موفقیت آمیز بود";
            response.Token = token;
            response.Id=user.Id;
            return response;
        }
    }
}
