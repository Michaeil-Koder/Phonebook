using FluentValidation;
using Phonebook.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Authentication.Queries.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(lgu => lgu.NationalCode)
                .NotNull().WithMessage("کد ملی باید فرستاده شود")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد")
                .MustAsync(async(NationalCode,cancelToken)=>await ExistNationalCode(NationalCode)).WithMessage("کد ملی یافت نشد.");
        }
        
        private async Task<bool> ExistNationalCode(string NationalCode)
        {
            return await _userRepository.ExistsNationalCode(NationalCode);
        }
    }
}
