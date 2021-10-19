using cs_aspnet_mvc_crud.Controllers;
using cs_aspnet_mvc_crud.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace cs_aspnet_mvc_crud.Middleware.Auth
{
    public class UserSession : ActionFilterAttribute
    {
        private user userModel;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);

                userModel = (user)HttpContext.Current.Session["field_user"];

                //Validar si no hay session redireccionar al login
                if (userModel == null)
                {
                    if (filterContext.Controller is AuthController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Auth/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }
        }
    }
}