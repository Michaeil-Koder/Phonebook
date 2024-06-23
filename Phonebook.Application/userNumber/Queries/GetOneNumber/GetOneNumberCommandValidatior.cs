using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Queries.GetOneNumber
{
    public class GetOneNumberCommandValidatior:AbstractValidator<GetOneNumberCommand>
    {
        public GetOneNumberCommandValidatior()
        {
            RuleFor(cu => cu.Title)
                .NotNull().WithMessage("عنوان باید فرستاده شود.")
                .NotEmpty().WithMessage("عنوان نباید خالی باشد.")
                .MinimumLength(3).WithMessage("عنوان باید حداقل داری 3 کاراکتر باشد.")
                .MaximumLength(20).WithMessage("عنوان باید حداکثر دارای 20 کاراکتر باشد.")
                ;
        }
    }
}
