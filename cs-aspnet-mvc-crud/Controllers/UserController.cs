using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;
using cs_aspnet_mvc_crud.Middleware.Auth;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        [UserAuthorization(userActionId: 26)]
        public async Task<ActionResult> Index()
        {
            var user = entityModel.User.Include(u => u.user_position);
            return View(await user.ToListAsync());
        }

        // GET: User/Details/5
        [UserAuthorization(userActionId: 27)]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await entityModel.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        [UserAuthorization(userActionId: 28)]
        public ActionResult Create()
        {
            ViewBag.user_position_id = new SelectList(entityModel.UserPosition, "id", "name");
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 28)]
        public async Task<ActionResult> Create([Bind(Include = "id,username,password,first_name,last_name,email,picture,birthdate,created_at,user_position_id")] user user)
        {
            if (ModelState.IsValid)
            {
                entityModel.User.Add(user);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_position_id = new SelectList(entityModel.UserPosition, "id", "name", user.user_position_id);
            return View(user);
        }

        // GET: User/Edit/5
        [UserAuthorization(userActionId: 29)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await entityModel.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_position_id = new SelectList(entityModel.UserPosition, "id", "name", user.user_position_id);
            return View(user);
        }

        // POST: User/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 29)]
        public async Task<ActionResult> Edit([Bind(Include = "id,username,password,first_name,last_name,email,picture,birthdate,created_at,user_position_id")] user user)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(user).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_position_id = new SelectList(entityModel.UserPosition, "id", "name", user.user_position_id);
            return View(user);
        }

        // GET: User/Delete/5
        [UserAuthorization(userActionId: 30)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = await entityModel.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization(userActionId: 30)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            user user = await entityModel.User.FindAsync(id);
            entityModel.User.Remove(user);
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
