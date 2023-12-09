using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb2.Controllers
{
    //[Route("accounts")]
    public class AccountsController : Controller
    {
        //[Route("google-login")] // co cai nay vi khi truy cap 1 controller can authen ma lai chua log in thi program.cs da dc cau hinh de den url nay
        public IActionResult GoogleLogin()
       {
            
            //var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // in case user already logged in and manually enter url Accounts/GoogleLogin
            if (User.Claims.Count() > 0) // when user logged in and has licences/claims
                //return View("~/Views/Books/Index.cshtml");
                return RedirectToAction("Index", "Books");
            else
            {
                var properties = new AuthenticationProperties { RedirectUri = Url.Action("Index", "Books") };
                return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            }
        }

        public async Task<IActionResult> GoogleLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[Route("google-response")]
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    var claims = result.Principal?.Identities?.FirstOrDefault()?
        //        .Claims.Select(c => new
        //        {
        //            c.Issuer,
        //            c.OriginalIssuer,
        //            c.Type,
        //            c.Value
        //        });

        //    return Json(claims);
        //}
    }
}
