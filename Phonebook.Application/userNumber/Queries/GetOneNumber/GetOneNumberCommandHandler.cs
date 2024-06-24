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

namespace Phonebook.Application.userNumber.Queries.GetOneNumber
{
    public class GetOneNumberCommandHandler : IRequestHandler<GetOneNumberCommand, BaseCommandResponse>
    {
        private readonly IValidator<GetOneNumberCommand> _validator;
        private readonly IChekSign_ExpToken _check;
        private readonly IUserNumbersRepository _userNumbers;

        public GetOneNumberCommandHandler(IValidator<GetOneNumberCommand> validator,IChekSign_ExpToken check,IUserNumbersRepository userNumbers)
        {
            _validator = validator;
            _check = check;
            _userNumbers = userNumbers;
        }

        public async Task<BaseCommandResponse> Handle(GetOneNumberCommand request, CancellationToken cancellationToken)
        {
            var resultCheckToken = await _check.Check();
            if (!resultCheckToken.Success) return resultCheckToken;

            var response = new BaseCommandResponse();

            var resultValidator=await _validator.ValidateAsync(request);
            if (!resultValidator.IsValid)
            {
                response.Status = 400;
                response.Success = false;
                response.Errors=resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }

            var resultNumber=await _userNumbers.GetOneNumberByUserIdAndTitle(resultCheckToken.Id.GetValueOrDefault(), request.Title);

            if (resultNumber == null) 
            {
                response.Status = 404;
                response.Success = false;
                response.Message = "شماره تلفنی یافت نشد.";
                return response;
            };

            response.Status = 200;
            response.Success = true;
            response.Message = "شماره تلفن یافت شد";
            response.Body = new Dictionary<string, string>();
            ((Dictionary<string, string>)response.Body).Add(resultNumber.Title, resultNumber.Phone);
            return response;
        }
    }
}
