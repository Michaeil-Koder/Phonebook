using Jose;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Phonebook.Application.Identity.JsonWebToken
{
    public class JsonWebToken : IJsonWebToken
    {
        private readonly IConfiguration _configuration;
        public JsonWebToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Guid Id)
        {
            int Day = int.Parse(_configuration["Jwt:Day"]);
            var payload = new Dictionary<string, object>()
                {
                    { "Id", Id },
                    { "exp", DateTime.UtcNow.AddDays(Day) }
                };

            var secretKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            return Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
        }
    }
}
