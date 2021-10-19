using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class UserActionController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: UserAction
        public async Task<ActionResult> Index()
        {
            var user_action = entityModel.user_action.Include(u => u.module);
            return View(await user_action.ToListAsync());
        }

        // GET: UserAction/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_action user_action = await entityModel.user_action.FindAsync(id);
            if (user_action == null)
            {
                return HttpNotFound();
            }
            return View(user_action);
        }

        // GET: UserAction/Create
        public ActionResult Create()
        {
            ViewBag.module_id = new SelectList(entityModel.module, "id", "name");
            return View();
        }

        // POST: UserAction/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,module_id")] user_action user_action)
        {
            if (ModelState.IsValid)
            {
                entityModel.user_action.Add(user_action);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.module_id = new SelectList(entityModel.module, "id", "name", user_action.module_id);
            return View(user_action);
        }

        // GET: UserAction/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_action user_action = await entityModel.user_action.FindAsync(id);
            if (user_action == null)
            {
                return HttpNotFound();
            }
            ViewBag.module_id = new SelectList(entityModel.module, "id", "name", user_action.module_id);
            return View(user_action);
        }

        // POST: UserAction/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,module_id")] user_action user_action)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(user_action).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.module_id = new SelectList(entityModel.module, "id", "name", user_action.module_id);
            return View(user_action);
        }

        // GET: UserAction/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_action user_action = await entityModel.user_action.FindAsync(id);
            if (user_action == null)
            {
                return HttpNotFound();
            }
            return View(user_action);
        }

        // POST: UserAction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            user_action user_action = await entityModel.user_action.FindAsync(id);
            entityModel.user_action.Remove(user_action);
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
