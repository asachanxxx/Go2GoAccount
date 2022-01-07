using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Go2Go.Web.Authentication
{
    public interface IJwtTokenManager
    {
        string Authenticate(string userName, string password);
    }
}
