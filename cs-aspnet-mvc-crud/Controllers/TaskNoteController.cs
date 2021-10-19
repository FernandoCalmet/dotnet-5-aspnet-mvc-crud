using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class TaskNoteController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: TaskNote
        public async Task<ActionResult> Index()
        {
            var task_note = entityModel.TaskNote.Include(t => t.note).Include(t => t.task);
            return View(await task_note.ToListAsync());
        }

        // GET: TaskNote/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task_note task_note = await entityModel.TaskNote.FindAsync(id);
            if (task_note == null)
            {
                return HttpNotFound();
            }
            return View(task_note);
        }

        // GET: TaskNote/Create
        public ActionResult Create()
        {
            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name");
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name");
            return View();
        }

        // POST: TaskNote/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,task_id,note_id")] task_note task_note)
        {
            if (ModelState.IsValid)
            {
                entityModel.TaskNote.Add(task_note);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name", task_note.note_id);
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name", task_note.task_id);
            return View(task_note);
        }

        // GET: TaskNote/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task_note task_note = await entityModel.TaskNote.FindAsync(id);
            if (task_note == null)
            {
                return HttpNotFound();
            }
            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name", task_note.note_id);
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name", task_note.task_id);
            return View(task_note);
        }

        // POST: TaskNote/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,task_id,note_id")] task_note task_note)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(task_note).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name", task_note.note_id);
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name", task_note.task_id);
            return View(task_note);
        }

        // GET: TaskNote/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            task_note task_note = await entityModel.TaskNote.FindAsync(id);
            if (task_note == null)
            {
                return HttpNotFound();
            }
            return View(task_note);
        }

        // POST: TaskNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            task_note task_note = await entityModel.TaskNote.FindAsync(id);
            entityModel.TaskNote.Remove(task_note);
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
