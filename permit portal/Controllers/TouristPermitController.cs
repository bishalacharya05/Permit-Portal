using Microsoft.AspNetCore.Mvc;
using permit_portal.Models;
namespace permit_portal.Controllers
{
    public class TouristPermitController : Controller
    {
        public IActionResult Apply()
        {
            return View();
        }

       
    }
}
