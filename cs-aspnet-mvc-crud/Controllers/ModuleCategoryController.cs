using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class ModuleCategoryController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: ModuleCategory
        public async Task<ActionResult> Index()
        {
            return View(await entityModel.ModuleCategory.ToListAsync());
        }

        // GET: ModuleCategory/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuleCategory/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
