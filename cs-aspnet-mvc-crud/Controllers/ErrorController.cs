using System.Web.Mvc;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}