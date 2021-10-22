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
    public class TaskController : BaseController
    {
        // GET: Task
        [UserAuthorization(userActionId: 31)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewBag.CreatedAtSortParm = String.IsNullOrEmpty(sortOrder) ? "created_at_desc" : "";
            ViewBag.UpdatedAtSortParm = String.IsNullOrEmpty(sortOrder) ? "updated_at_desc" : "";
            ViewBag.UserUsernameSortParm = String.IsNullOrEmpty(sortOrder) ? "user_username_desc" : "";
            ViewBag.UserEmailSortParm = String.IsNullOrEmpty(sortOrder) ? "user_email_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tasks = from o in entityModel.Task select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(o =>
                    o.name.Contains(searchString)
                    || o.description.Contains(searchString)
                    || o.user.username.Contains(searchString)
                    || o.user.email.Contains(searchString)
                );
            }

            switch (sortOrder)
            {
                case "id_desc":
                    tasks = tasks.OrderByDescending(o => o.id);
                    break;
                case "name_desc":
                    tasks = tasks.OrderByDescending(o => o.name);
                    break;
                case "description_desc":
                    tasks = tasks.OrderByDescending(o => o.description);
                    break;
                case "status_desc":
                    tasks = tasks.OrderByDescending(o => o.status);
                    break;
                case "created_at_desc":
                    tasks = tasks.OrderByDescending(o => o.created_at);
                    break;
                case "updated_at_desc":
                    tasks = tasks.OrderByDescending(o => o.updated_at);
                    break;
                case "user_username_desc":
                    tasks = tasks.OrderByDescending(o => o.user.username);
                    break;
                case "user_email_desc":
                    tasks = tasks.OrderByDescending(o => o.user.email);
                    break;
                default:
                    tasks = tasks.OrderBy(o => o.id);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View(tasks.ToPagedList(pageNumber, this.pageSize));
        }

        // GET: Task
        [UserAuthorization(userActionId: 31)]
        public async Task<ActionResult> GetAll()
        {
            var task = entityModel.Task.Include(t => t.user);
            return View(await task.ToListAsync());
        }

        // GET: Task/Details/5
        [UserAuthorization(userActionId: 32)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = await entityModel.Task.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        [UserAuthorization(userActionId: 33)]
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username");
            return View();
        }

        // POST: Task/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 33)]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description,status,created_at,updated_at,user_id")] task task)
        {
            if (ModelState.IsValid)
            {
                entityModel.Task.Add(task);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(entityModel.User, "id", "username", task.user_id);
            return View(task);
        }

        // GET: Task/Edit/5
        [UserAuthorization(userActionId: 34)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = await entityModel.Task.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username", task.user_id);
            return View(task);
        }

        // POST: Task/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 34)]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description,status,created_at,updated_at,user_id")] task task)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(task).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username", task.user_id);
            return View(task);
        }

        // GET: Task/Delete/5
        [UserAuthorization(userActionId: 35)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = await entityModel.Task.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 35)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            task task = await entityModel.Task.FindAsync(id);
            entityModel.Task.Remove(task);
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
