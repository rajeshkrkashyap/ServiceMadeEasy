using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceMadeEasy.In.Models;

namespace ServiceMadeEasy.In.Controllers
{
    [Authorize]
    public class PackagesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Packages

        public async Task<ActionResult> Index()
        {
            return View(await db.Packages.ToListAsync());
        }

        // GET: Packages/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Packages.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }


        public async Task<ActionResult> Buy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!User.Identity.IsAuthenticated)
            {
               return RedirectToAction("Login", "Account");
            }

            Package package = await db.Packages.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }
        // GET: Packages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Packages.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Package package = await db.Packages.FindAsync(id);
            db.Packages.Remove(package);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
