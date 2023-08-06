using Module.Users.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Core.Interfaces
{
    public interface IAdminAuthenticationRepository
    {
        Task<RegisterDto> AddNewAdmin(RegisterDto adminDto);
    }
}
