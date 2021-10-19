using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;
using cs_aspnet_mvc_crud.Middleware.Auth;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class ModuleController : BaseController
    {
        // GET: Module
        [UserAuthorization(userActionId: 6)]
        public async Task<ActionResult> Index()
        {
            var module = entityModel.Module.Include(m => m.module_category);
            return View(await module.ToListAsync());
        }

        // GET: Module/Details/5
        [UserAuthorization(userActionId: 7)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module module = await entityModel.Module.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Module/Create
        [UserAuthorization(userActionId: 8)]
        public ActionResult Create()
        {
            ViewBag.module_category_id = new SelectList(entityModel.ModuleCategory, "id", "name");
            return View();
        }

        // POST: Module/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 8)]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description,module_category_id")] module module)
        {
            if (ModelState.IsValid)
            {
                entityModel.Module.Add(module);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.module_category_id = new SelectList(entityModel.ModuleCategory, "id", "name", module.module_category_id);
            return View(module);
        }

        // GET: Module/Edit/5
        [UserAuthorization(userActionId: 9)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module module = await entityModel.Module.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.module_category_id = new SelectList(entityModel.ModuleCategory, "id", "name", module.module_category_id);
            return View(module);
        }

        // POST: Module/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 9)]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description,module_category_id")] module module)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(module).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.module_category_id = new SelectList(entityModel.ModuleCategory, "id", "name", module.module_category_id);
            return View(module);
        }

        // GET: Module/Delete/5
        [UserAuthorization(userActionId: 10)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            module module = await entityModel.Module.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 10)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            module module = await entityModel.Module.FindAsync(id);
            entityModel.Module.Remove(module);
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
