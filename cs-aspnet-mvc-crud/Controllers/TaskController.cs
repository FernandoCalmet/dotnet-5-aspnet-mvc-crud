using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;
using cs_aspnet_mvc_crud.Middleware.Auth;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class TaskController : BaseController
    {
        // GET: Task
        [UserAuthorization(userActionId: 31)]
        public async Task<ActionResult> Index()
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
