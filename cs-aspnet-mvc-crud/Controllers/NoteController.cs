using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;
using cs_aspnet_mvc_crud.Middleware.Auth;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class NoteController : BaseController
    {
        // GET: Note
        [UserAuthorization(userActionId: 36)]
        public async Task<ActionResult> Index()
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
        public async Task<ActionResult> Create([Bind(Include = "id,name,description,created_at,updated_at")] note note)
        {
            if (ModelState.IsValid)
            {
                entityModel.Note.Add(note);
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
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description,created_at,updated_at")] note note)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(note).State = EntityState.Modified;
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
