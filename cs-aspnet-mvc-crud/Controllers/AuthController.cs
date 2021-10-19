using System;
using System.Linq;
using System.Web.Mvc;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(string userField, string passField)
        {
            try
            {
                using (Models.DataBaseEntities entityModel = new Models.DataBaseEntities())
                {
                    var userModel = (
                        from u in entityModel.User
                        where
                            u.email == userField.Trim()
                            || u.username == userField.Trim()
                            && u.password == passField.Trim()
                        select u
                    ).FirstOrDefault();

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

        // GET: Logout
        public ActionResult Logout()
        {
            Session["field_user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}