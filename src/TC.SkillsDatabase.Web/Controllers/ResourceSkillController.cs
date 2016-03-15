namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Core.Models.DbModels;
    using DAL;

    public class ResourceSkillController : Controller
    {
        private SkillsDatabaseContext db = new SkillsDatabaseContext();

        // GET: ResourceSkill
        public ActionResult Index()
        {
            var resourceSkills = db.ResourceSkills.Include(r => r.Resource).Include(r => r.Skill).Include(r => r.SkillLevel);
            return View(resourceSkills.ToList());
        }

        // GET: ResourceSkill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResourceSkill resourceSkill = db.ResourceSkills.Find(id);
            if (resourceSkill == null)
            {
                return HttpNotFound();
            }

            return View(resourceSkill);
        }

        // GET: ResourceSkill/Create
        public ActionResult Create()
        {
            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name");
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name");
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "Id", "Name");
            return View();
        }

        // POST: ResourceSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ResourceId,SkillId,SkillLevelId,SkillLevelValue")] ResourceSkill resourceSkill)
        {
            if (ModelState.IsValid)
            {
                db.ResourceSkills.Add(resourceSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name", resourceSkill.ResourceId);
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name", resourceSkill.SkillId);
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "Id", "Name", resourceSkill.SkillLevelId);
            return View(resourceSkill);
        }

        // GET: ResourceSkill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResourceSkill resourceSkill = db.ResourceSkills.Find(id);
            if (resourceSkill == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name", resourceSkill.ResourceId);
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name", resourceSkill.SkillId);
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "Id", "Name", resourceSkill.SkillLevelId);
            return View(resourceSkill);
        }

        // POST: ResourceSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ResourceId,SkillId,SkillLevelId,SkillLevelValue")] ResourceSkill resourceSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resourceSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResourceId = new SelectList(db.Resources, "Id", "Name", resourceSkill.ResourceId);
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name", resourceSkill.SkillId);
            ViewBag.SkillLevelId = new SelectList(db.SkillLevels, "Id", "Name", resourceSkill.SkillLevelId);
            return View(resourceSkill);
        }

        // GET: ResourceSkill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ResourceSkill resourceSkill = db.ResourceSkills.Find(id);
            if (resourceSkill == null)
            {
                return HttpNotFound();
            }

            return View(resourceSkill);
        }

        // POST: ResourceSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResourceSkill resourceSkill = db.ResourceSkills.Find(id);
            db.ResourceSkills.Remove(resourceSkill);
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
