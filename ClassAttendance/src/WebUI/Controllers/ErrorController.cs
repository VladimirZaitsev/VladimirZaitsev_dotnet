using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(ErrorViewModel model)
        {
            return View(model);
        }
    }
}
