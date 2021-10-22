using System;
using System.Linq;
using System.Web.Mvc;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class AuthController : Controller
    {
        //GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // GET: Auth Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string field_user, string field_pass)
        {
            try
            {
                using (Models.DBEntities entityModel = new Models.DBEntities())
                {
                    var userModel = (
                        from u in entityModel.User 
                        where u.email == field_user.Trim() || u.username == field_user.Trim() && u.password_hash == field_pass.Trim()
                        select u).FirstOrDefault();

                    if (userModel == null)
                    {
                        ViewBag.Error = "The user or password is not valid.";
                        return View();
                    }

                    Session["field_user"] = userModel;
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Auth Logout
        public ActionResult Logout()
        {
            Session["field_user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}