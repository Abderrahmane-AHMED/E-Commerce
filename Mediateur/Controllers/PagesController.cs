using Mediateur.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mediateur.Controllers
{
    public class PagesController : Controller
    {
        IPages oPages;
        public PagesController(IPages pages)
        { 
              oPages = pages;
        }
        public IActionResult Index(int pageId)
        {
            var page = oPages.GetById(pageId);

            if (page == null)
            {
                
                return NotFound("Page Not Found.");
            }

            return View(page);
        }
    }
}
