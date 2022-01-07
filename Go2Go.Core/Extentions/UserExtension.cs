using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Core.Extentions
{
    public static class UserExtension
    {
        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {
            return user.GetClaimsIdentity().IsAuthenticated;
        }

        public static ClaimsIdentity GetClaimsIdentity(this ClaimsPrincipal principal)
        {
            return (ClaimsIdentity)principal.Identity;
        }

        //public static bool HasRole(this ClaimsIdentity user, string role)
        //{
        //    return user.HasClaim(OkloClaimTypes.Role, role);
        //}

        public static ClaimsIdentity IssueClaim(this ClaimsIdentity identity, string type, string value)
        {
            identity.AddClaim(new Claim(type, value));
            return identity;
        }

        public static ClaimsIdentity IssueIfNotDefault<T>(this ClaimsIdentity identity, string type, T value)
        {
            if (!object.Equals(value, default(T)))
            {
                identity.IssueClaim(type, value.ToString());
            }

            return identity;
        }
        public static string GetLoginId(this ClaimsPrincipal user)
        {
            return user.GetNameIdentifier();
        }

        public static string GetGivenName(this ClaimsPrincipal user)
        {
            return user.GetClaimsIdentity().GetClaimValue(ClaimTypes.GivenName);
        }

        public static string GetCompany(this ClaimsPrincipal user)
        {
            return user.GetClaimsIdentity().GetClaimValue(Go2GoClaimTypes.CompanyAlias);
        }

        public static int GetCompanyId(this ClaimsPrincipal user)
        {
            var value = user.GetClaimsIdentity().GetClaimValue(Go2GoClaimTypes.CompanyId);
            return value != null ? int.Parse(value) : 0;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.GetClaimsIdentity().GetClaimValue(Go2GoClaimTypes.UserId);
            return value != null ? int.Parse(value) : 0;
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.GetClaimsIdentity().GetClaimValue(Go2GoClaimTypes.Role);
        }
        public static string GetCompanyTypeName(this ClaimsPrincipal user)
        {
            return user.GetClaimValue(Go2GoClaimTypes.CompanyTypeName);
        }

        public static string GetClaimValue(this ClaimsPrincipal user, string type)
        {
            return user.GetClaimsIdentity().GetClaimValue(type);
        }

        public static string GetSurname(this ClaimsPrincipal user)
        {
            return user.GetClaimsIdentity().GetClaimValue(ClaimTypes.Surname);
        }
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.GetClaimsIdentity().GetClaimValue(Go2GoClaimTypes.UserName);
        }
        public static string GetNameIdentifier(this ClaimsPrincipal identity)
        {
            return identity.GetClaimsIdentity().GetClaimValue(ClaimTypes.NameIdentifier);
        }

        public static string GetName(this ClaimsPrincipal identity)
        {
            return identity.GetClaimsIdentity().GetClaimValue(JwtClaimTypes.Name);
        }

        public static string GetEmail(this ClaimsPrincipal identity)
        {
            return identity.GetClaimsIdentity().GetClaimValue(JwtClaimTypes.Email);
        }
        public static string GetClaimValue(this ClaimsIdentity identity, string claimType)
        {
            return identity.FindFirst(claimType)?.Value;
        }

        public static string GetClientIP(this ClaimsPrincipal identity)
        {
            return identity.GetClaimsIdentity().GetClaimValue(Go2GoClaimTypes.ClientIP);
        }
    }
}
