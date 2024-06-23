using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Responses
{
    public class BaseCommandResponse
    {
        public Guid? Id { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; } = 200;
        public Dictionary<string, string>? Body { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public string? Token { get; set; }
        
    }
}
