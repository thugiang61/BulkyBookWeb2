using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb2.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult GoogleLogin()
       {            
            // in case user already logged in and manually enter url Accounts/GoogleLogin
            if (User.Claims.Count() > 0) 
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
    }
}
