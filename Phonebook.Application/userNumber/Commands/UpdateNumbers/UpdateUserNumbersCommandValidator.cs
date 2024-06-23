using FluentValidation;
using Phonebook.Application.DTOs;
using Phonebook.Application.IRepository;

namespace Phonebook.Application.userNumber.Commands.UpdateNumbers
{
    public class UpdateUserNumbersCommandValidator:AbstractValidator<GenericUserNumbersDto>
    {
        private readonly IUserNumbersRepository _userNumbersRepository;

        public UpdateUserNumbersCommandValidator(IUserNumbersRepository userNumbersRepository)
        {
            _userNumbersRepository = userNumbersRepository;

            RuleFor(cu => cu.Title)
                .NotNull().WithMessage("عنوان باید فرستاده شود.")
                .NotEmpty().WithMessage("عنوان نباید خالی باشد.")
                .MinimumLength(3).WithMessage("عنوان باید حداقل داری 3 کاراکتر باشد.")
                .MaximumLength(20).WithMessage("عنوان باید حداکثر دارای 20 کاراکتر باشد.")
                ;

            RuleFor(cu => cu.Phone)
                .NotNull().WithMessage("تلفن باید فرستاده شود.")
                .Matches(@"^(\+98|0098|0)?(9\d{9}|[1-8]\d{9})$").WithMessage("شماره تلفن را درست وارد کنید.");
        }
    }
}
