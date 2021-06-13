using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Login.Ecommerce
{
    public class LoginManager
    {
        protected readonly IHttpContextAccessor _HttpContextAccessor;
        public LoginManager(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;

        }

        public string GetCurrentUserId()
        {
            if (!_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;
            var UserId = _HttpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            if (UserId != null)
            {
                return UserId.Value;
            }
            return null;

        }


        public async Task LoginAsync(IEnumerable<Claim> claims)
        {
            #region How to use AuthenticationProperties
            // var authProperties = new AuthenticationProperties
            // {
            //AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            //IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
            // };
            #endregion
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );

            await _HttpContextAccessor.HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,

               new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
               {
                   IsPersistent = true,
                   ExpiresUtc = DateTime.UtcNow.AddDays(1)
               }); ;
        }

        public async Task LoginWithDefaultClaimsAsync(DefaultClaims defualtClaims)
        {

            if (!defualtClaims.IsValidClaim)
                throw new Exception("The claim is not valid");

            var claims = new List<Claim>();

            claims.AddRange(defualtClaims.Roles.Select(x => new Claim(ClaimTypes.Role, x)));

            if (defualtClaims.IsUserNameSet)
                claims.Add(new Claim(ClaimTypes.Name, defualtClaims.UserName));

            if (defualtClaims.IsEmailSet)
                claims.Add(new Claim(ClaimTypes.Email, defualtClaims.Email));

            if (defualtClaims.IsIdSet)
                claims.Add(new Claim(ClaimTypes.NameIdentifier, defualtClaims.UserId));

            await LoginAsync(claims);

        }

        public HashSet<string> GetCurrentUserRoles()
        {
            if (!_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return new HashSet<string>();
            var roles = _HttpContextAccessor.HttpContext.User.FindAll(x => x.Type == ClaimTypes.Role);
            return new HashSet<string>(roles.Select(x => x.Value));
        }

        public bool Islogged()
        {
            return _HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        }

        public DefaultClaims GetDefaultClaims()
        {
            if (!_HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            return new DefaultClaims
            {
                Roles = GetCurrentUserRoles(),
                Email = _HttpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value,
                UserName = _HttpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value,
                UserId = _HttpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value
            };
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
    }
