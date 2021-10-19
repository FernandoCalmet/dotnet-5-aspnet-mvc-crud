using cs_aspnet_mvc_crud.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace cs_aspnet_mvc_crud.Middleware.Auth
{
    public class Authorization : AuthorizeAttribute
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class VerificarAutorizacion : AuthorizeAttribute
        {
            private user userModel;
            private DataBaseEntities entityModel = new DataBaseEntities();
            private int userActionId;

            public VerificarAutorizacion(int userActionId = 0)
            {
                this.userActionId = userActionId;
            }

            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                String userActionName = "";
                String moduleName = "";

                try
                {
                    userModel = (user)HttpContext.Current.Session["field_user"];
                    var userActionsList = from p in entityModel.UserPermission
                                          where p.user_position_id == userModel.user_position_id
                                          && p.user_action_id == userActionId
                                          select p;

                    if (userActionsList.ToList().Count() == 0)
                    {
                        var userActionModel = entityModel.UserAction.Find(userActionId);
                        int? moduleId = userActionModel.module_id;
                        userActionName = GetUserActionName(userActionId);
                        moduleName = GetModuleName(moduleId);
                        filterContext.Result = new RedirectResult("~/Auth/Index?action=" + userActionName);
                    }
                }
                catch (Exception)
                {
                    filterContext.Result = new RedirectResult("~/Auth/Index?action=" + userActionName);
                }
            }

            public string GetUserActionName(int userActionId)
            {
                var userAction = from a in entityModel.UserAction
                                 where a.id == userActionId
                                 select a.name;

                String userActionName;

                try
                {
                    userActionName = userAction.First();
                }
                catch (Exception)
                {
                    userActionName = "";
                }

                return userActionName;
            }

            public string GetModuleName(int? moduleId)
            {
                var module = from m in entityModel.Module
                             where m.id == moduleId
                             select m.name;

                String moduleName;

                try
                {
                    moduleName = module.First();
                }
                catch (Exception)
                {
                    moduleName = "";
                }

                return moduleName;
            }
        }
    }
}