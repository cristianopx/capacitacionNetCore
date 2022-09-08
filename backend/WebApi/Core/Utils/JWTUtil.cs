using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class JWTUtil
    {
        public static string? GetTokenUsername(ClaimsIdentity? identity)
        {
            if(identity == null)
                return null;
            IEnumerable<Claim> claims = identity.Claims;
            foreach(var claim in claims)
            {
                if (claim.Type == "username")
                    return claim.Value;
            }
            return null;
        }
    }
}
