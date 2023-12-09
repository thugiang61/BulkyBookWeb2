using BulkyBookWeb2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb2.Controllers
{
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        public HomeController()
        {
            //_logger = logger;
        }

        public IActionResult Index()
        {
            //ViewData["UserName"] = User.FindFirstValue(ClaimTypes.Name); // !! ViewData chi truyen tu 1 controller ts 1 view th, nen BooksController co ViewData thi HomeController\Index cx ko sdung dc nen la phai tao va truyen 1 ViewData ms, nhung nhu v vi du them controller thi phai them dong nay, vi ViewData["UserName"] cx anh hg ts _Layout.cshtml nx
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}