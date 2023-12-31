
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;
using Admin.Contract.Interface.Implementation;
using Admin.Contract.Models.User;
using Admin.Contract.Models.HttpRequestResponse;

namespace Admin.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepositry _userDataService;

        public AccountController(IUserRepositry userDataService)
        {

            _userDataService = userDataService;
        }
        public async Task<ActionResult> Login()
        {
            if (Request.Cookies.TryGetValue("User_Code", out string cookieValue))
            {
                var userId = Request.Cookies["User_Code"];
                var password = await _userDataService.DecryptAsync( Request.Cookies["Password"]);
                //}
                //    if (UserCode != null)
                //{
                var user = await _userDataService.LoginUserAsync(userId, password);
                if (user == null || user.Password != password)
                {
                    ModelState.AddModelError("", "Invalid username or password");

                    return RedirectToAction("Login", "Account");
                }

            //    var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.User_Code.ToString()),
            //    new Claim(ClaimTypes.Name, user.First_Name),
            //    new Claim(ClaimTypes.Role, user.Role),
            //    new Claim(ClaimTypes.Email, password),

            //};

            //    Response.Cookies.Append("User_Code", userId);
            //    Response.Cookies.Append("Password",  password);
            //    Response.Cookies.Append("Password", await _userDataService.EncryptAsync(user.Password));
            //    Response.Cookies.Append("COMP_CODE", user.COMP_CODE);

            //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //    var authProperties = new AuthenticationProperties();

            //    await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity), authProperties);



                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginUser loginuser)
        {
            var massage = "";
            var user = await _userDataService.LoginUserAsync(loginuser.User_Code, loginuser.Password);
            if (user == null || user.Password != loginuser.Password)
            {
                ModelState.AddModelError("", "Invalid username or password");
                massage = "Invalid username or password";
                return Json(new ResponseDataEnvelop<string>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = "Invalid username or password"
                });
            }

            string pswen = await _userDataService.EncryptAsync(loginuser.Password);
            string pswdc = await _userDataService.DecryptAsync(pswen);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.User_Code.ToString()),
                new Claim(ClaimTypes.Name, user.First_Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.SerialNumber, user.User_Code),
                new Claim(ClaimTypes.Surname, user.Password),
            };
            var cookieOptions = new CookieOptions
            {
                Secure = false, // Send cookies only over HTTPS
                SameSite = SameSiteMode.None // Allow cross-origin cookies
            };

            Response.Cookies.Append("User_Code", loginuser.User_Code);
            Response.Cookies.Append("Password", await _userDataService.EncryptAsync(loginuser.Password));
            Response.Cookies.Append("COMP_CODE", user.COMP_CODE);

            TempData["User_Code"] = user.User_Code;

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
            massage = "Sucess";

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("User_Code");
            Response.Cookies.Delete("Password");
            Response.Cookies.Delete("COMP_CODE");

            
            return RedirectToAction("Login", "Account");
        }
    }
}
