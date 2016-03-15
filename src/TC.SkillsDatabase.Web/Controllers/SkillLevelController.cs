namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Core.Models.DbModels;
    using DAL;

    public class SkillLevelController : Controller
    {
        private SkillsDatabaseContext db = new SkillsDatabaseContext();

        // GET: SkillLevel
        public ActionResult Index()
        {
            return View(db.SkillLevels.ToList());
        }

        // GET: SkillLevel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SkillLevel skillLevel = db.SkillLevels.Find(id);
            if (skillLevel == null)
            {
                return HttpNotFound();
            }

            return View(skillLevel);
        }

        // GET: SkillLevel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Value,IsForLanguageSkill")] SkillLevel skillLevel)
        {
            if (ModelState.IsValid)
            {
                db.SkillLevels.Add(skillLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillLevel);
        }

        // GET: SkillLevel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SkillLevel skillLevel = db.SkillLevels.Find(id);
            if (skillLevel == null)
            {
                return HttpNotFound();
            }

            return View(skillLevel);
        }

        // POST: SkillLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Value,IsForLanguageSkill")] SkillLevel skillLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillLevel);
        }

        // GET: SkillLevel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SkillLevel skillLevel = db.SkillLevels.Find(id);
            if (skillLevel == null)
            {
                return HttpNotFound();
            }

            return View(skillLevel);
        }

        // POST: SkillLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillLevel skillLevel = db.SkillLevels.Find(id);
            db.SkillLevels.Remove(skillLevel);
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
