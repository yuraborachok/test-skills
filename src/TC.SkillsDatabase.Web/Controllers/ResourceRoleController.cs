namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Core.Models.DbModels;
    using DAL;

    public class ResourceRoleController : Controller
    {
        private SkillsDatabaseContext db = new SkillsDatabaseContext();

        // GET: ResourceRole
        public ActionResult Index()
        {
            return View(db.ResourceRoles.ToList());
        }

        // GET: ResourceRole/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResourceRole resourceRole = db.ResourceRoles.Find(id);
            if (resourceRole == null)
            {
                return HttpNotFound();
            }

            return View(resourceRole);
        }

        // GET: ResourceRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResourceRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ResourceRole resourceRole)
        {
            if (ModelState.IsValid)
            {
                db.ResourceRoles.Add(resourceRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resourceRole);
        }

        // GET: ResourceRole/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResourceRole resourceRole = db.ResourceRoles.Find(id);
            if (resourceRole == null)
            {
                return HttpNotFound();
            }

            return View(resourceRole);
        }

        // POST: ResourceRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ResourceRole resourceRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resourceRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resourceRole);
        }

        // GET: ResourceRole/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResourceRole resourceRole = db.ResourceRoles.Find(id);
            if (resourceRole == null)
            {
                return HttpNotFound();
            }

            return View(resourceRole);
        }

        // POST: ResourceRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResourceRole resourceRole = db.ResourceRoles.Find(id);
            db.ResourceRoles.Remove(resourceRole);
            db.SaveChanges();
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
