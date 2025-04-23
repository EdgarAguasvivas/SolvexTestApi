using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string username, string role);
    }
}
