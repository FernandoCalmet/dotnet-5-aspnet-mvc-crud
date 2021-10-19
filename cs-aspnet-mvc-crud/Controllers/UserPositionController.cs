using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using cs_aspnet_mvc_crud.Models;

namespace cs_aspnet_mvc_crud.Controllers
{
    public class UserPositionController : Controller
    {
        private DataBaseEntities entityModel = new DataBaseEntities();

        // GET: UserPosition
        public async Task<ActionResult> Index()
        {
            return View(await entityModel.UserPosition.ToListAsync());
        }

        // GET: UserPosition/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_position user_position = await entityModel.UserPosition.FindAsync(id);
            if (user_position == null)
            {
                return HttpNotFound();
            }
            return View(user_position);
        }

        // GET: UserPosition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserPosition/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description")] user_position user_position)
        {
            if (ModelState.IsValid)
            {
                entityModel.UserPosition.Add(user_position);
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user_position);
        }

        // GET: UserPosition/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_position user_position = await entityModel.UserPosition.FindAsync(id);
            if (user_position == null)
            {
                return HttpNotFound();
            }
            return View(user_position);
        }

        // POST: UserPosition/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description")] user_position user_position)
        {
            if (ModelState.IsValid)
            {
                entityModel.Entry(user_position).State = EntityState.Modified;
                await entityModel.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user_position);
        }

        // GET: UserPosition/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_position user_position = await entityModel.UserPosition.FindAsync(id);
            if (user_position == null)
            {
                return HttpNotFound();
            }
            return View(user_position);
        }

        // POST: UserPosition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            user_position user_position = await entityModel.UserPosition.FindAsync(id);
            entityModel.UserPosition.Remove(user_position);
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
