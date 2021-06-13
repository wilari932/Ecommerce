using Login.Ecommerce;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthExtencion
    {
        // extions methods 
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services, Action<CookieAuthenticationOptions> callback = default)
        {
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			 .AddCookie(
			  CookieAuthenticationDefaults.AuthenticationScheme,
			 (options) => {
				 options.LoginPath = new PathString("/account/login");
				 options.LogoutPath = new PathString("/account/logout");
				 options.AccessDeniedPath = new PathString("/account/notallowed");
				 options.ReturnUrlParameter = "returnUrl";
				 callback?.Invoke(options);
	
				});
			services.AddTransient<LoginManager>();


			return services;
		}
       
    }
}
