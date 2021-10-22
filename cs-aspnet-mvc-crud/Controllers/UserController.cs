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
    public class UserController : BaseController
    {
        // GET: User
        [UserAuthorization(userActionId: 26)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.UsernameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";
            ViewBag.EmailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.CreatedAtSortParm = String.IsNullOrEmpty(sortOrder) ? "created_at_desc" : "";
            ViewBag.UserPositionNameSortParm = String.IsNullOrEmpty(sortOrder) ? "user_position_name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from o in entityModel.User select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(o =>
                    o.username.Contains(searchString)
                    || o.first_name.Contains(searchString)
                    || o.last_name.Contains(searchString)
                    || o.email.Contains(searchString)
                    || o.user_position.name.Contains(searchString)
                );
            }

            switch (sortOrder)
            {
                case "id_desc":
                    users = users.OrderByDescending(o => o.id);
                    break;
                case "username_desc":
                    users = users.OrderByDescending(o => o.username);
                    break;
                case "first_name_desc":
                    users = users.OrderByDescending(o => o.first_name);
                    break;
                case "last_name_desc":
                    users = users.OrderByDescending(o => o.last_name);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(o => o.email);
                    break;
                case "created_at_desc":
                    users = users.OrderByDescending(o => o.created_at);
                    break;
                case "user_position_name_desc":
                    users = users.OrderByDescending(o => o.user_position.name);
                    break;
                default:
                    users = users.OrderBy(o => o.id);
                    break;
            }
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, this.pageSize));
        }

        // GET: User
        [UserAuthorization(userActionId: 26)]
        public async Task<ActionResult> GetAll()
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
        public async Task<ActionResult> Create([Bind(Include = "id,username,email,email_confirmed,password_hash,security_stamp,two_factor_enabled,lockout_end_date_utc,lockout_enabled,access_failed,first_name,last_name,picture,birthdate,created_at,user_position_id")] user user)
        {
            if (ModelState.IsValid)
            {
                entityModel.User.Add(user);
                user.email_confirmed = false;
                user.security_stamp = null;
                user.two_factor_enabled = false;
                user.lockout_end_date_utc = null;
                user.lockout_enabled = false;
                user.access_failed_count = 0;
                user.created_at = DateTime.Now.ToUniversalTime();
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
        public async Task<ActionResult> Edit([Bind(Include = "id,username,email,email_confirmed,password_hash,security_stamp,two_factor_enabled,lockout_end_date_utc,lockout_enabled,access_failed,first_name,last_name,picture,birthdate,user_position_id")] user user)
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
