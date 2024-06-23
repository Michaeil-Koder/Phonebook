using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Phonebook.Application.Responses;
using Jose;
using System.Text;
using System.Text.Json;


namespace Phonebook.Application.Identity.CheckToken
{
    public class ChekSign_ExpToken:IChekSign_ExpToken
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;

        public ChekSign_ExpToken(IHttpContextAccessor httpContext,IConfiguration configuration)
        {
            _httpContext = httpContext;
            _configuration = configuration;
        }

        public BaseCommandResponse Check()
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
