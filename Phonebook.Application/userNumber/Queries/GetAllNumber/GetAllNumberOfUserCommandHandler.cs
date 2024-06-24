using MediatR;
using Phonebook.Application.DTOs;
using Phonebook.Application.Identity.CheckToken;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Queries.GetAllNumber
{
    public class GetAllNumberOfUserCommandHandler : IRequestHandler<GetAllNumberOfUserCommand, BaseCommandResponse>
    {
        private readonly IChekSign_ExpToken _chek;
        private readonly IUserNumbersRepository _userNumbers;

        public GetAllNumberOfUserCommandHandler(IChekSign_ExpToken chek,IUserNumbersRepository userNumbers)
        {
            _chek = chek;
            _userNumbers = userNumbers;
        }

        public async Task<BaseCommandResponse> Handle(GetAllNumberOfUserCommand request, CancellationToken cancellationToken)
        {
            var resultCheckToken = await _chek.Check();
            if (!resultCheckToken.Success)
            {
                return resultCheckToken;
            }

            var response=new BaseCommandResponse();

            var UserNumberList= await _userNumbers.GetUserNumbersByUserId(resultCheckToken.Id.GetValueOrDefault());
            if (UserNumberList.Count==0)
            {
                response.Status = 404;
                response.Success = false;
                response.Message = "شماره ای ثبت نشده است.";
                return response;
            }
            response.Success=true;
            response.Status=200;
            response.Message = "با موفقیت شماره ها گرفته شد.";
            response.Body = new Dictionary<string, string>();
            foreach (var userNumber in UserNumberList)
            {
                ((Dictionary<string, string>)response.Body).Add(userNumber.Title,userNumber.Phone);
            }
            return response;
        }
    }
}
