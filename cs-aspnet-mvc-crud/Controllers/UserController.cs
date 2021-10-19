using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class UserController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: User
        public async Task<ActionResult> Index()
        {
            var user = entityModel.user.Include(u => u.user_position);
            return View(await user.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await entityModel.user.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name");
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,username,password,first_name,last_name,email,picture,birthdate,created_at,user_position_id")] user user)
        {
            if (ModelState.IsValid)
            {
                entityModel.user.Add(user);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name", user.user_position_id);
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await entityModel.user.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name", user.user_position_id);
            return View(user);
        }

        // POST: User/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,username,password,first_name,last_name,email,picture,birthdate,created_at,user_position_id")] user user)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(user).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_position_id = new SelectList(entityModel.user_position, "id", "name", user.user_position_id);
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await entityModel.user.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            user user = await entityModel.user.FindAsync(id);
            entityModel.user.Remove(user);
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
