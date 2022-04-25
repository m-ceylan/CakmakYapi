using Cakmak.Yapi.Core.Security;
using Cakmak.Yapi.Repository.ProjectUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cakmak.Yapi.Presentation.Controllers
{
    public class LoginController : Controller
    {

        private readonly UserRepository repo;
        public LoginController(UserRepository _repo)
        {
            repo = _repo;
        }
        public IActionResult Index()

        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "HomeM", new { Area = "admin" });
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SignIn(IFormCollection frm)
        {
            string email = frm["txtEmail"];
            string password = frm["txtPassword"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["Info"] = "Kullanıcı adı ve ya parola boş olamaz";

                return RedirectToAction("Index", "Login");
            }

            var user = await repo.FirstOrDefaultByAsync(x => x.Email == email && x.Password == new Cryptography().EncryptString(password));

            if (user == null)
            {
                TempData["Info"] = "Hatalı giriş.";
                return RedirectToAction("Index", "Login");
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
            new Claim("userId",user.Id.ToString()),
            new Claim("firstName",user.FirstName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "HomeM", new { Area = "admin" });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
