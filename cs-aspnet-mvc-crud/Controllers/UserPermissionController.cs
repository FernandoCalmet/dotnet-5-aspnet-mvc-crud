using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class UserPermissionController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: UserPermission
        public async Task<ActionResult> Index()
        {
            var user_permission = entityModel.user_permission.Include(u => u.user_action).Include(u => u.user_position);
            return View(await user_permission.ToListAsync());
        }

        // GET: UserPermission/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_permission user_permission = await entityModel.user_permission.FindAsync(id);
            if (user_permission == null)
            {
                return HttpNotFound();
            }
            return View(user_permission);
        }

        // GET: UserPermission/Create
        public ActionResult Create()
        {
            ViewBag.user_action_id = new SelectList(entityModel.user_action, "id", "name");
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name");
            return View();
        }

        // POST: UserPermission/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,user_position_id,user_action_id")] user_permission user_permission)
        {
            if (ModelState.IsValid)
            {
                entityModel.user_permission.Add(user_permission);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_action_id = new SelectList(entityModel.user_action, "id", "name", user_permission.user_action_id);
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name", user_permission.user_position_id);
            return View(user_permission);
        }

        // GET: UserPermission/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_permission user_permission = await entityModel.user_permission.FindAsync(id);
            if (user_permission == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_action_id = new SelectList(entityModel.user_action, "id", "name", user_permission.user_action_id);
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name", user_permission.user_position_id);
            return View(user_permission);
        }

        // POST: UserPermission/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,user_position_id,user_action_id")] user_permission user_permission)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(user_permission).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_action_id = new SelectList(entityModel.user_action, "id", "name", user_permission.user_action_id);
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name", user_permission.user_position_id);
            return View(user_permission);
        }

        // GET: UserPermission/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_permission user_permission = await entityModel.user_permission.FindAsync(id);
            if (user_permission == null)
            {
                return HttpNotFound();
            }
            return View(user_permission);
        }

        // POST: UserPermission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            user_permission user_permission = await entityModel.user_permission.FindAsync(id);
            entityModel.user_permission.Remove(user_permission);
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
