using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

 namespace Mediateur.Areas.admin.Controllers
{
    [Authorize(Roles = "Manager , Admin , Data Entry")]
    [Area("admin")]
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
