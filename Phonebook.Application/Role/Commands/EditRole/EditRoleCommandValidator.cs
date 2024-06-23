using FluentValidation;
using Phonebook.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Role.Commands.EditRole
{
    public class EditRoleCommandValidator:AbstractValidator<EditRoleCommand>
    {
        private readonly IRoleRepository _role;

        public EditRoleCommandValidator(IRoleRepository role)
        {
            _role = role;


            RuleFor(r => r.RoleName)
                .NotNull().WithMessage("نقش حتما باید فرستاده شود")
                .NotEmpty().WithMessage("نقش را وارد کنید")
                .MustAsync(async (Role, cancelToken) => await CheckExistRole(Role)).WithMessage("این نقش وجود دارد");
        }

        private async Task<bool> CheckExistRole(string role)
        {
            return !await _role.ExistRole(role);
        }
    }
}
