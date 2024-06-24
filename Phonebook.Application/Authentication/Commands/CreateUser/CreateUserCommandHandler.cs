using AutoMapper;
using FluentValidation;
using MediatR;
using Phonebook.Application.DTOs;
using Phonebook.Application.Identity.JsonWebToken;
using Phonebook.Application.Identity.PasswordHasher;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using Phonebook.Application.userNumber.Commands.CreateUserNumbers;
using Phonebook.Domain.Entities;



namespace Phonebook.Application.Authentication.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IJsonWebToken _jsonWebToken;
        private readonly IMediator _mediator;
        private readonly IValidator<GenericUserNumbersDto> _validator2;
        private readonly IUserNumbersRepository _userNumbers;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserCommand> validator , IJsonWebToken jsonWebToken , IMediator mediator , IValidator<GenericUserNumbersDto> validator2,IUserNumbersRepository userNumbers)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _jsonWebToken = jsonWebToken;
            _mediator = mediator;
            _validator2 = validator2;
            _userNumbers = userNumbers;
        }

        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            var response = new BaseCommandResponse();

            #region Validation
            var ValidationResult = await _validator.ValidateAsync(request);
            if (!ValidationResult.IsValid)
            {
                //throw new ValidationException(ValidationResult.Errors);
                response.Success = false;
                response.Message = "خطایی رخ داده است.";
                response.Errors = ValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }
            #endregion

           

            #region CreateUser
            var user = _mapper.Map<User>(request);

            var countUser = await _userRepository.GetCount();
            //user.UserRoles.

            var HashPas=PasswordHasher.HashPassword(user.Password);
            user.Password = HashPas;

            await _userRepository.Create(user);
            #endregion

            #region AddTitlePhones
            if (request.TitlePhone != null)
            {
                foreach (var TitlePhone in request.TitlePhone)
                {

                    var userNum = new UserNumbers()
                    {
                        Phone = TitlePhone.Value,
                        Title = TitlePhone.Key,
                        UserId = user.Id,
                    };

                    var UserDto = _mapper.Map<GenericUserNumbersDto>(userNum);

                    #region Validation
                    var ValidationResultUserNumbers = await _validator2.ValidateAsync(UserDto);
                    if (!ValidationResultUserNumbers.IsValid)
                    {
                        //throw new ValidationException(ValidationResult.Errors);
                        response.Success = false;
                        response.Status = 400;
                        response.Message = "خطایی رخ داده است.";
                        response.Errors = ValidationResultUserNumbers.Errors.Select(x => x.ErrorMessage).ToList();
                        return response;
                    }
                    #endregion

                    await _userNumbers.Create(userNum);

                }

            }
            #endregion

            var token=_jsonWebToken.GenerateToken(user.Id);

            response.Success = true;
            response.Message = "با موفقیت کاربر ایجاد شد";
            response.Id = user.Id;
            response.Token = token;
            return response;
        }
    }
}
