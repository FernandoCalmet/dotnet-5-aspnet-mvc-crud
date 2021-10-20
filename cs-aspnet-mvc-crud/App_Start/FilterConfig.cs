using System.Web.Mvc;

namespace cs_aspnet_mvc_crud
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Middleware.Auth.UserSession());
        }
    }
}
