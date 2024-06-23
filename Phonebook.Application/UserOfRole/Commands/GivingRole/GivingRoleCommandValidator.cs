using FluentValidation;
using Phonebook.Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.UserOfRole.Commands.GivingRole
{
    public class GivingRoleCommandValidator:AbstractValidator<GivingRoleCommand>
    {
        private readonly IRoleRepository _role;
        private readonly IUserRepository _user;
        private readonly IUserRoleRepository _userRole;

        public GivingRoleCommandValidator(IRoleRepository role,IUserRepository user,IUserRoleRepository userRole)
        {
            RuleFor(gr=>gr.RoleId)
                .NotNull().WithMessage("آیدی نقش باید فرستاده شود.")
                .NotEmpty().WithMessage("آیدی نقش باید پر شود.")
                .MustAsync(async(RoleId,cancelationToken)=>await ExistRoleId(RoleId)).WithMessage("آیدی نقش وجود ندارد.")
                ;

            RuleFor(gr => gr.UserId)
                .NotNull().WithMessage("آیدی کاربر باید فرستاده شود.")
                .NotEmpty().WithMessage("آیدی کاربر باید پر شود.")
                .MustAsync(async (UserId, cancelationToken) => await ExistUserId(UserId)).WithMessage("آیدی کاربر وجود ندارد.")
                ;

            RuleFor(gr => gr)
                .MustAsync(async (gr, cancelationToken) => await ExistUserRole(gr.RoleId,gr.UserId)).WithMessage("این نقش برای این کاربر وجود دارد.")
                ;

            _role = role;
            _user = user;
            _userRole = userRole;
        }

        private async Task<bool> ExistUserRole(Guid roleId, Guid userId)
        {
            return ! await _userRole.ExistUserRoleByUserId_RoleId(userId, roleId);
        }

        private async Task<bool> ExistUserId(Guid UserId)
        {
            return await _user.Exist(UserId);
        }

        private async Task<bool> ExistRoleId(Guid roleId)
        {
            return await _role.Exist(roleId);
        }
    }
}
