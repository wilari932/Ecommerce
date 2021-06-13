using Ecommerce.Models;
using Login.Ecommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
   
		[AllowAnonymous]
		public class AccountController : Controller
		{
			private readonly LoginManager _loginManager;

			public AccountController(LoginManager loginManager) 
			{
				_loginManager = loginManager;
			}


			public IActionResult Login(string returnUrl = "/")
			{
				if (!_loginManager.Islogged())
				{
					ViewData["returnUrl"] = returnUrl;

					return View();
				}
				return Redirect("/");



			}
			[HttpPost]
			public async Task<IActionResult> Login([FromForm] UserLoginModel model, [FromQuery] string returnUrl = "/")
			{
				try
				{

					
					if (!_loginManager.Islogged())
					{

					
						await _loginManager.LoginWithDefaultClaimsAsync(new DefaultClaims { 
						
							Email = model.UserName,
							UserName = model.UserName,
							Roles = new List<string> {"Admin" }
						});
					
						
					}
					return Redirect("/");
				}
				catch (Exception e)
				{
					return Content("<span>Hubo un errror!</span>");
				}


			}
			//public IActionResult Register(string refer = "/")
			//{
			//	var model = PrepareModel<UserRegisterViewModel>();

			//	return View(model);
			//}
			//[HttpPost]
			//[ValidateModel]
			//public async Task<IActionResult> Register([FromBody] UserRegisterModel model, string refer)
			//{
			//	if (!HttpContext.Request.Cookies.ContainsKey("RegisterOnce"))
			//	{
			//		try
			//		{

			//			model.Roles = new HashSet<string>(new string[] { "Subscriber" });
			//			await _UserService.Register(model);
			//			await _UserService.Login(model.UserName, model.Password);
			//			HttpContext.Response.Cookies.Append("RegisterOnce", DateTime.Now.ToString());
			//			return Ok(new { Message = "yes" });
			//		}
			//		catch (Exception e)
			//		{
			//			return StatusCode(500, new JsonResponseModel<object>(ErrorResponse.New(e.Message)));
			//		}

			//	}
			//	return StatusCode(500, new JsonResponseModel<object>(ErrorResponse.New("Perdon pero en esta version solo podras crear una cuenta profavor contactame en facebook")));
			//}
			//[HttpGet]
			//public IActionResult ForgotPasswoord()
			//{

			//	var model = PrepareModel<ForgotPasswordModel>();
			//	return View(model);


			//}
			//[HttpPost]
			//[ValidateModel]
			//public async Task<IActionResult> ForgotPassWoord([FromBody] ForgotPasswordDataModel model)
			//{
			//	try
			//	{
			//		await _UserService.RegisterForgotPassword(model.Email);
			//		return Ok(new { message = "Succes" });
			//	}
			//	catch (Exception e)
			//	{
			//		return StatusCode(500, new JsonResponseModel<object>(ErrorResponse.New(string.Format("Error:{0} \n  Stack:{1}", e.Message, e.StackTrace))));
			//	}
			//}

			//public async Task<IActionResult> Recover([FromQuery] string token)
			//{
			//	var model = PrepareModel<RecoveryPasswordModel>();
			//	try
			//	{
			//		await _UserService.ValidateMeta(token);
			//		model.ErrorOnToken = false;

			//		return View(model);
			//	}
			//	catch (Exception e)
			//	{
			//		model.ErrorOnToken = true;
			//		return View(model);
			//	}
			//}
			//[HttpPost]
			//[ValidateModel]
			//public async Task<IActionResult> Recover([FromBody] RecoveryPasswordDataModel model, [FromQuery] string token)
			//{
			//	try
			//	{
			//		await _UserService.ChangePasswordWithToken(token, model.PassWord);

			//		return Ok(new { message = "Succes" });
			//	}
			//	catch (Exception e)
			//	{
			//		return StatusCode(500, new JsonResponseModel<object>(ErrorResponse.New("Server Error")));
			//	}
			//}

			//[HttpGet]
			//[Authorize]
			//public IActionResult Logout()
			//{
			//	if (_UserService.Islogged())
			//	{
			//		return View(PrepareModel<BaseDataModel<object>>());
			//	}
			//	return Redirect("/account/login");
			//}

			//[HttpPost]
			//[Authorize]
			//public async Task<IActionResult> PostLogout()
			//{
			//	if (_UserService.Islogged())
			//	{
			//		await HttpContext.SignOutAsync(
			//					CookieAuthenticationDefaults.AuthenticationScheme);
			//		return Ok(new { Message = "Succes" });
			//	}
			//	return StatusCode(500, new JsonResponseModel<object>(ErrorResponse.New("Server Error")));
			//}
			//public IActionResult Notallowed()
			//{
			//	return Content("<span>No estas autorizado para entrar aqui!</span>");
			//}

		}
	}

