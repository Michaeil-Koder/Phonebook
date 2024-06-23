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

namespace Phonebook.Application.userNumber.Commands.CreateUserNumbers
{
    public class CreateUserNumbersCommandHandler : IRequestHandler<CreateUserNumbersCommand, BaseCommandResponse>
    {
        private readonly IUserNumbersRepository _userNumbersRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GenericUserNumbersDto> _validator;
        private readonly IChekSign_ExpToken _chek;

        public CreateUserNumbersCommandHandler(IUserNumbersRepository userNumbersRepository, IMapper mapper, IValidator<GenericUserNumbersDto> validator, IChekSign_ExpToken chek)
        {
            _userNumbersRepository = userNumbersRepository;
            _mapper = mapper;
            _validator = validator;
            _chek = chek;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserNumbersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _chek.Check();
                if (result.Success == false)
                {
                    return result;
                }

                Guid userId = result.Id.GetValueOrDefault();

                var response = new BaseCommandResponse();

                foreach (var TitlePhone in request.TitlePhone)
                {

                    var userNum = new UserNumbers()
                    {
                        Phone = TitlePhone.Value,
                        Title = TitlePhone.Key,
                        UserId = userId,
                    };

                    var UserDto = _mapper.Map<GenericUserNumbersDto>(userNum);

                    #region Validation
                    var ValidationResult = await _validator.ValidateAsync(UserDto);
                    if (!ValidationResult.IsValid)
                    {
                        //throw new ValidationException(ValidationResult.Errors);
                        response.Success = false;
                        response.Status = 400;
                        response.Message = "خطایی رخ داده است.";
                        response.Errors = ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                        return response;
                    }
                    #endregion

                    await _userNumbersRepository.Create(userNum);

                }


                response.Success = true;
                response.Status = 201;
                response.Message = "با موفقیت ایجاد شد";

                return response;


            }
            catch
            {
                var response = new BaseCommandResponse();
                response.Success = false;
                response.Message = "خطایی روی داده است.";
                return response;
            }
        }
    }
}
