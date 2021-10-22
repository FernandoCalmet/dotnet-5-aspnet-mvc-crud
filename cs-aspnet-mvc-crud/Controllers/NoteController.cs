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
    public class NoteController : BaseController
    {
        // GET: Note
        [UserAuthorization(userActionId: 36)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CreatedAtSortParm = String.IsNullOrEmpty(sortOrder) ? "created_at_desc" : "";
            ViewBag.UpdatedAtSortParm = String.IsNullOrEmpty(sortOrder) ? "updated_at_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var notes = from o in entityModel.Note select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                notes = notes.Where(o => o.name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    notes = notes.OrderByDescending(o => o.id);
                    break;
                case "name_desc":
                    notes = notes.OrderByDescending(o => o.name);
                    break;
                case "created_at_desc":
                    notes = notes.OrderByDescending(o => o.created_at);
                    break;
                case "updated_at_desc":
                    notes = notes.OrderByDescending(o => o.updated_at);
                    break;
                default:
                    notes = notes.OrderBy(o => o.id);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View(notes.ToPagedList(pageNumber, this.pageSize));
        }

        // GET: Note
        [UserAuthorization(userActionId: 36)]
        public async Task<ActionResult> GetAll()
        {
            return View(await entityModel.Note.ToListAsync());
        }

        // GET: Note/Details/5
        [UserAuthorization(userActionId: 37)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            note note = await entityModel.Note.FindAsync(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Note/Create
        [UserAuthorization(userActionId: 38)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 38)]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description,created_at")] note note)
        {
            if (ModelState.IsValid)
            {
                entityModel.Note.Add(note);
                note.created_at = DateTime.Now.ToUniversalTime();
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(note);
        }

        // GET: Note/Edit/5
        [UserAuthorization(userActionId: 39)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            note note = await entityModel.Note.FindAsync(id);
            note.updated_at = DateTime.Now.ToUniversalTime();
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Note/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 39)]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description,updated_at")] note note)
        {           
            if (ModelState.IsValid)
            {                
                entityModel.Entry(note).State = EntityState.Modified;
                note.updated_at = DateTime.Now.ToUniversalTime();
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(note);
        }

        // GET: Note/Delete/5
        [UserAuthorization(userActionId: 40)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            note note = await entityModel.Note.FindAsync(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 40)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            note note = await entityModel.Note.FindAsync(id);
            entityModel.Note.Remove(note);
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
