using FluentValidation;
using Phonebook.Application.DTOs;
using Phonebook.Application.IRepository;
using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Authentication.Commands.CreateUser
{
    public class ValidatorForUserNumbers : AbstractValidator<GenericUserNumbersDto>
    {
        private readonly IUserNumbersRepository _userNumbersRepository;
        private readonly IUserRepository _user;

        public ValidatorForUserNumbers(IUserNumbersRepository userNumbersRepository, IUserRepository user)
        {
            _userNumbersRepository = userNumbersRepository;
            _user = user;
            RuleFor(cu => cu.Title)
                .NotNull().WithMessage("عنوان باید فرستاده شود.")
                .NotEmpty().WithMessage("عنوان نباید خالی باشد.")
                .MinimumLength(3).WithMessage("عنوان باید حداقل داری 3 کاراکتر باشد.")
                .MaximumLength(20).WithMessage("عنوان باید حداکثر دارای 20 کاراکتر باشد.")
                ;

            RuleFor(cu => cu.Phone)
                .NotNull().WithMessage("تلفن باید فرستاده شود.")
                .Matches(@"^(\+98|0098|0)?(9\d{9}|[1-8]\d{9})$").WithMessage("شماره تلفن را درست وارد کنید.");

            RuleFor(cu => cu)
                .MustAsync(async (cu, cancelation) =>
                {
                    return await ExistTitlePhone(cu.UserId, cu.Title, cu.Phone);
                }).WithMessage("قبلا این تلفن در سیستم ثبت شده است.");
        }

        private async Task<bool> ExistUser(Guid userId)
        {
            return await _user.Exist(userId);
        }

        private async Task<bool> ExistTitlePhone(Guid UserId, string Title, string Phone)
        {
            return !await _userNumbersRepository.ExistTitlePhone(UserId, Title, Phone);
        }
    }
}
