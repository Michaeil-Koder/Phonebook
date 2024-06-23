using AutoMapper;
using FluentValidation;
using MediatR;
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
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<CreateUserCommand> validator , IJsonWebToken jsonWebToken , IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
            _jsonWebToken = jsonWebToken;
            _mediator = mediator;
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
                var userNumCommand = new CreateUserNumbersCommand()
                {
                    TitlePhone = request.TitlePhone,
                    //UserId = user.Id,
                };

                var result= await _mediator.Send(userNumCommand);
                if (!result.Success)
                {
                    return result;
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
