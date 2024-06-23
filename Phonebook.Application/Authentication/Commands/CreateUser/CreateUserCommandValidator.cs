using FluentValidation;
using Phonebook.Application.IRepository;

namespace Phonebook.Application.Authentication.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository) 
        {
            _userRepository = userRepository;

            RuleFor(u => u.FirstName)
                .NotNull().WithMessage("نام اجباری است.")
                .NotEmpty().WithMessage("نام اجباری است.")
                .MinimumLength(4).WithMessage("نام باید حداقل باید دارای 4 کاراکتر باشد")
                .MaximumLength(25).WithMessage("نام باید حداکثر دارای 25 کاراکتر باشد.");

            RuleFor(u => u.LastName)
                .NotNull().WithMessage("نام خانوادگی اجباری است.")
                .NotEmpty().WithMessage("نام خانوادگی اجباری است.")
                .MinimumLength(5).WithMessage("نام خانوادگی باید حداقل باید دارای 5 کاراکتر باشد")
                .MaximumLength(30).WithMessage("نام خانوادگی باید حداکثر دارای 30 کاراکتر باشد.");

            RuleFor(u => u.NationalCode)
                .NotNull().WithMessage("کد ملی اجباری است")
                .NotEmpty().WithMessage("کد ملی اجباری است")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد")
                .MustAsync(async (NationalCode,cancelation)=>await ExistsNationalCode(NationalCode)).WithMessage("این کد ملی در سیستم موجود است.");

            RuleFor(u => u.Gender)
                .NotNull().WithMessage("جنسیت باید یکی از مقادیر تعریف شده باشد.")
                .IsInEnum().WithMessage("جنسیت باید یکی از مقادیر تعریف شده باشد.");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("پسوورد حتما باید فرستاده شود")
                .MinimumLength(8).WithMessage("پسوورد حداقل باید 8 کاراکتر باشد.")
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$").WithMessage("پسوورد باید دارای یک حرف کوچک، یک حرف بزرگ و یک عدد باشد.");
                
        }

        private async Task<bool> ExistsNationalCode(string NationalCode)
        {
            
            return !await _userRepository.ExistsNationalCode(NationalCode);
        }
    }
}
