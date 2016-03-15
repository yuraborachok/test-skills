namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Core.Models.DbModels;
    using DAL;

    public class ResourceController : Controller
    {
        private SkillsDatabaseContext db = new SkillsDatabaseContext();

        // GET: Resource
        public ActionResult Index()
        {
            var resources = db.Resources.Include(r => r.Location).Include(r => r.ResourceRole).Include(r => r.Team);
            return View(resources.ToList());
        }

        // GET: Resource/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }

            return View(resource);
        }

        // GET: Resource/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.ResourceRoleId = new SelectList(db.ResourceRoles, "Id", "Name");
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: Resource/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TeamId,LocationId,ResourceRoleId,Manager")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", resource.LocationId);
            ViewBag.ResourceRoleId = new SelectList(db.ResourceRoles, "Id", "Name", resource.ResourceRoleId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", resource.TeamId);
            return View(resource);
        }

        // GET: Resource/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", resource.LocationId);
            ViewBag.ResourceRoleId = new SelectList(db.ResourceRoles, "Id", "Name", resource.ResourceRoleId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", resource.TeamId);
            return View(resource);
        }

        // POST: Resource/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TeamId,LocationId,ResourceRoleId,Manager")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", resource.LocationId);
            ViewBag.ResourceRoleId = new SelectList(db.ResourceRoles, "Id", "Name", resource.ResourceRoleId);
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", resource.TeamId);
            return View(resource);
        }

        // GET: Resource/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }

            return View(resource);
        }

        // POST: Resource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
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
