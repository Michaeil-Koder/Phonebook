using AutoMapper;
using MediatR;
using Phonebook.Application.DTOs.Role;
using Phonebook.Application.Identity.CheckToken;
using Phonebook.Application.IRepository;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Role.Queries.GetAllRole
{
    public class GetAllRoleCommandHandler : IRequestHandler<GetAllRoleCommand, BaseCommandResponse>
    {
        private readonly IChekSign_ExpToken _check;
        private readonly IRoleRepository _role;
        private readonly IMapper _mapper;

        public GetAllRoleCommandHandler(IChekSign_ExpToken Check,IRoleRepository role,IMapper mapper)
        {
            _check = Check;
            _role = role;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(GetAllRoleCommand request, CancellationToken cancellationToken)
        {
            #region Check Token For Auth
            var resultCheckToken = _check.Check();
            if (!resultCheckToken.Success)
            {
                return resultCheckToken;
            }
            #endregion

            var response=new BaseCommandResponse();

            var GetAllRole=await _role.GetAll();
            if(GetAllRole.Count == 0)
            {
                response.Status = 404;
                response.Success = false;
                response.Message = "نقشی یافت نشد.";
                return response;
            }
            response.Success= true;
            response.Body = _mapper.Map<List<GetAllRoleDto>>(GetAllRole);
            return response;

        }
    }
}
