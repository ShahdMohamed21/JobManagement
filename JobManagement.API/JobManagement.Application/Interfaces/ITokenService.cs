using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync( string userId,string email,IList<string> roles);
    }
}
