using FluentValidation;
using MediatR;
using Phonebook.Application.Identity.CheckToken;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Commands.DeleteUserNumber
{
    public class DeleteUserNumberCommandHandler : IRequestHandler<DeleteUserNumberCommand, BaseCommandResponse>
    {
        private readonly IValidator<DeleteUserNumberCommand> _validator;
        private readonly IUserNumbersRepository _userNumbers;
        private readonly IChekSign_ExpToken _check;

        public DeleteUserNumberCommandHandler(IValidator<DeleteUserNumberCommand> validator,IUserNumbersRepository userNumbers,IChekSign_ExpToken Check)
        {
            _validator = validator;
            _userNumbers = userNumbers;
            _check = Check;
        }

        public async Task<BaseCommandResponse> Handle(DeleteUserNumberCommand request, CancellationToken cancellationToken)
        {

            var resultCheckToken=_check.Check();
            if(!resultCheckToken.Success)
            {
                return resultCheckToken;
            }
            var response = new BaseCommandResponse();

            #region Validator for Title
            var resultValidator =await _validator.ValidateAsync(request, cancellationToken);
            if(!resultValidator.IsValid)
            {
                response.Status = 400;
                response.Success = false;
                response.Message = "عنوان را به درستی وارد کنید.";
                response.Errors=resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }
            #endregion

            var FindNumberOfUser = await _userNumbers.GetOneNumberByUserIdAndTitle(resultCheckToken.Id.GetValueOrDefault(), request.Title);
            if(FindNumberOfUser == null)
            {
                response.Status = 404;
                response.Success = false;
                response.Message = "شماره ای با مشخصات داده شده یافت نشد.";
                return response;
            }

            var DeleteNumberResult = await _userNumbers.Delete(FindNumberOfUser.Id);
            if (!DeleteNumberResult)
            {
                response.Status = 400;
                response.Success = false;
                response.Message = "به دلیل خطا های رخ داده حذف رخ نداد.";
                return response;
            }

            response.Success = true;
            response.Message = "با موفقیت حذف شد";
            return response;

        }
    }
}
