using AutoMapper;
using FluentValidation;
using MediatR;
using Phonebook.Application.DTOs;
using Phonebook.Application.Identity.CheckToken;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Commands.UpdateNumbers
{
    public class UpdateUserNumbersCommandHandler : IRequestHandler<UpdateUserNumbersCommand, BaseCommandResponse>
    {
        private readonly IValidator<GenericUserNumbersDto> _validator;
        private readonly IChekSign_ExpToken _check;
        private readonly IMapper _mapper;
        private readonly IUserNumbersRepository _userNumbers;

        public UpdateUserNumbersCommandHandler(IValidator<GenericUserNumbersDto> validator,IChekSign_ExpToken check,IMapper mapper,IUserNumbersRepository userNumbers)
        {
            _validator = validator;
            _check = check;
            _mapper = mapper;
            _userNumbers = userNumbers;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUserNumbersCommand request, CancellationToken cancellationToken)
        {
            var resultCheckToken = await _check.Check();
            if (!resultCheckToken.Success)
            {
                return resultCheckToken;
            }
            var response=new BaseCommandResponse();

            foreach(var TitlePhone in request.GetUserNumberDto.TitlePhone)
            {
                var userNum = new UserNumbers()
                {
                    Title = TitlePhone.Key,
                    Phone = TitlePhone.Value,
                };
                var resultMap=_mapper.Map<GenericUserNumbersDto>(userNum);
                #region Validator On Request
                var resultValidator =await _validator.ValidateAsync(resultMap);
                if (!resultValidator.IsValid)
                {
                    response.Status = 400;
                    response.Success = false;
                    response.Message = "خطایی روی داده است";
                    response.Errors=resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
                    return response;
                }
                #endregion

                var resGetUserNumber=await _userNumbers.GetOneNumberByUserIdAndTitle(resultCheckToken.Id.GetValueOrDefault(), userNum.Title);
                if (resGetUserNumber==null)
                {
                    response.Status = 404;
                    response.Success = false;
                    response.Message = "شماره ای با این عنوان یافت نشد";
                    return response;
                }
                resGetUserNumber.Title = userNum.Title;
                resGetUserNumber.Phone = userNum.Phone;

                #region Update UserNumber
                var resultUpdate =await _userNumbers.Update(resGetUserNumber);
                if (!resultUpdate)
                {
                    response.Status = 400;
                    response.Success = false;
                    response.Message = "خطایی روی داده است";
                    return response;
                }
                #endregion
            }

            response.Success=true;
            response.Status=200;
            response.Message = "با موفقیت تمام موارد آپدیت شد";
            return response;
        }
    }
}
