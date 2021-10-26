using System.Web.Mvc;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.isAdmin = isAdmin;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.isAdmin = isAdmin;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.isAdmin = isAdmin;
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}