using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Phonebook.Application.Responses;
using Jose;
using System.Text;
using System.Text.Json;
using Phonebook.Application.IRepository;


namespace Phonebook.Application.Identity.CheckToken
{
    public class ChekSign_ExpToken:IChekSign_ExpToken
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _user;

        public ChekSign_ExpToken(IHttpContextAccessor httpContext,IConfiguration configuration,IUserRepository user)
        {
            _httpContext = httpContext;
            _configuration = configuration;
            _user = user;
        }

        public async Task<BaseCommandResponse> Check()
        {
            var response = new BaseCommandResponse();
            var Token = _httpContext.HttpContext.Request.Cookies["Token"];
            if (Token == null)
            {
                response.Status = 401;
                response.Success = false;
                response.Message = "برای دسترسی به این بخش ورود/ثبت نام انجام دهید";
                return response;
            }
            var secret= _configuration["Jwt:Key"];
            try
            {

                var result=Jose.JWT.Verify(Token,Encoding.UTF8.GetBytes(secret));
                token tokenSerialize=JsonSerializer.Deserialize<token>(result);


                var FindUser=await _user.Exist(tokenSerialize.Id);
                if (!FindUser)
                {
                    response.Status = 403;
                    response.Success = false;
                    response.Message = "شما به این بخش دسترسی ندارید.";
                    return response;
                }
                if(tokenSerialize.exp<DateTime.UtcNow) 
                {
                    response.Status = 401;
                    response.Success = false;
                    response.Message = "برای دسترسی به این بخش ورود/ثبت نام انجام دهید";
                    return response;
                }
                response.Success = true;
                response.Id = tokenSerialize.Id;
                return response;
                
            }catch(Exception ex)
            {
                response.Success=false;
                response.Message=ex.Message;
                response.Status = 401;
                return response;
            }
            
        }
    }

    public class token()
    {
        public Guid Id { get; set; }
        public DateTime exp { get; set; }
    }
}
