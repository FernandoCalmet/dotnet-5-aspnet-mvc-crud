using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using cs_aspnet_mvc_crud.Models;
using cs_aspnet_mvc_crud.Middleware.Auth;
using PagedList;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class ModuleCategoryController : BaseController
    {  
        // GET: ModuleCategory
        [UserAuthorization(userActionId: 1)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var modulesCategories = from o in entityModel.ModuleCategory select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                modulesCategories = modulesCategories.Where(o => o.name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    modulesCategories = modulesCategories.OrderByDescending(o => o.id);
                    break;
                case "name_desc":
                    modulesCategories = modulesCategories.OrderByDescending(o => o.name);
                    break;
                default:
                    modulesCategories = modulesCategories.OrderBy(o => o.id);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View(modulesCategories.ToPagedList(pageNumber, this.pageSize));
        }

        //GET: ModuleCategory
        [UserAuthorization(userActionId: 1)]
        public async Task<ActionResult> GetAll()
        {
            return View(await entityModel.ModuleCategory.ToListAsync());
        }

        // GET: ModuleCategory/Details/5
        [UserAuthorization(userActionId: 2)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module_category module_category = await entityModel.ModuleCategory.FindAsync(id);
            if (module_category == null)
            {
                return HttpNotFound();
            }
            return View(module_category);
        }

        // GET: ModuleCategory/Create
        [UserAuthorization(userActionId: 3)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuleCategory/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 3)]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description")] module_category module_category)
        {
            if (ModelState.IsValid)
            {
                entityModel.ModuleCategory.Add(module_category);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(module_category);
        }

        // GET: ModuleCategory/Edit/5
        [UserAuthorization(userActionId: 4)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module_category module_category = await entityModel.ModuleCategory.FindAsync(id);
            if (module_category == null)
            {
                return HttpNotFound();
            }
            return View(module_category);
        }

        // POST: ModuleCategory/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 4)]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description")] module_category module_category)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(module_category).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(module_category);
        }

        // GET: ModuleCategory/Delete/5
        [UserAuthorization(userActionId: 5)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module_category module_category = await entityModel.ModuleCategory.FindAsync(id);
            if (module_category == null)
            {
                return HttpNotFound();
            }
            return View(module_category);
        }

        // POST: ModuleCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 5)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            module_category module_category = await entityModel.ModuleCategory.FindAsync(id);
            entityModel.ModuleCategory.Remove(module_category);
            await entityModel.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entityModel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
