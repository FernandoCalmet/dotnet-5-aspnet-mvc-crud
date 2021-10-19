using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class TaskController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: Task
        public async Task<ActionResult> Index()
        {
            var task = entityModel.task.Include(t => t.user);
            return View(await task.ToListAsync());
        }

        // GET: Task/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = await entityModel.task.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(entityModel.user, "id", "username");
            return View();
        }

        // POST: Task/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description,status,created_at,updated_at,user_id")] task task)
        {
            if (ModelState.IsValid)
            {
                entityModel.task.Add(task);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(entityModel.user, "id", "username", task.user_id);
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = await entityModel.task.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(entityModel.user, "id", "username", task.user_id);
            return View(task);
        }

        // POST: Task/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description,status,created_at,updated_at,user_id")] task task)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(task).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(entityModel.user, "id", "username", task.user_id);
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task task = await entityModel.task.FindAsync(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            task task = await entityModel.task.FindAsync(id);
            entityModel.task.Remove(task);
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
