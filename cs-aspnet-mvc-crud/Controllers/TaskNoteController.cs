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
    public class TaskNoteController : BaseController
    {
        // GET: TaskNote
        [UserAuthorization(userActionId: 41)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.TaskNameSortParm = String.IsNullOrEmpty(sortOrder) ? "task_name_desc" : "";
            ViewBag.NoteNameSortParm = String.IsNullOrEmpty(sortOrder) ? "note_name_desc" : "";
            ViewBag.UserUsernameSortParm = String.IsNullOrEmpty(sortOrder) ? "user_username_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tasksNotes = from o in entityModel.TaskNote select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                tasksNotes = tasksNotes.Where(o =>
                    o.task.name.Contains(searchString)
                    || o.note.name.Contains(searchString)
                    || o.user.username.Contains(searchString)
                );
            }

            switch (sortOrder)
            {
                case "id_desc":
                    tasksNotes = tasksNotes.OrderByDescending(o => o.id);
                    break;
                case "task_name_desc":
                    tasksNotes = tasksNotes.OrderByDescending(o => o.task.name);
                    break;
                case "note_name_desc":
                    tasksNotes = tasksNotes.OrderByDescending(o => o.note.name);
                    break;
                case "user_username_desc":
                    tasksNotes = tasksNotes.OrderByDescending(o => o.user.username);
                    break;
                default:
                    tasksNotes = tasksNotes.OrderBy(o => o.id);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View(tasksNotes.ToPagedList(pageNumber, this.pageSize));
        }

        // GET: TaskNote
        [UserAuthorization(userActionId: 41)]
        public async Task<ActionResult> GetAll()
        {
            var task_note = entityModel.TaskNote.Include(t => t.note).Include(t => t.task).Include(t => t.user);
            return View(await task_note.ToListAsync());
        }

        // GET: TaskNote/Details/5
        [UserAuthorization(userActionId: 42)]
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
        [UserAuthorization(userActionId: 43)]
        public ActionResult Create()
        {
            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name");
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name");
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username");
            return View();
        }

        // POST: TaskNote/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 43)]
        public async Task<ActionResult> Create([Bind(Include = "id,task_id,note_id,user_id")] task_note task_note)
        {
            if (ModelState.IsValid)
            {
                entityModel.TaskNote.Add(task_note);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name", task_note.note_id);
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name", task_note.task_id);
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username", task_note.user_id);
            return View(task_note);
        }

        // GET: TaskNote/Edit/5
        [UserAuthorization(userActionId: 44)]
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
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username", task_note.user_id);
            return View(task_note);
        }

        // POST: TaskNote/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 44)]
        public async Task<ActionResult> Edit([Bind(Include = "id,task_id,note_id,user_id")] task_note task_note)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(task_note).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.note_id = new SelectList(entityModel.Note, "id", "name", task_note.note_id);
            ViewBag.task_id = new SelectList(entityModel.Task, "id", "name", task_note.task_id);
            ViewBag.user_id = new SelectList(entityModel.User, "id", "username", task_note.user_id);
            return View(task_note);
        }

        // GET: TaskNote/Delete/5
        [UserAuthorization(userActionId: 45)]
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
        [UserAuthorization(userActionId: 45)]
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
