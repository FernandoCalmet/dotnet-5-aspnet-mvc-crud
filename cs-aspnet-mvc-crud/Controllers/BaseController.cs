using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public abstract class BaseController : Controller
    {
        protected DataBaseEntities entityModel = new DataBaseEntities();
        public readonly int pageSize = 10;
    }
}