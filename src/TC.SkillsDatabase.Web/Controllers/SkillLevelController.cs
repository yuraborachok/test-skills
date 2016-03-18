namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using BL.Interfaces;
    using Core;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using Helpers;

    public class SkillLevelController : BaseAbstractController
    {
        private readonly ISkillLevelService skillLevelService;

        public SkillLevelController(ISkillLevelService skillLevelService)
        {
            this.skillLevelService = skillLevelService;
        }

        // GET: SkillLevel
        [Roles(CustomRoles.Admin, CustomRoles.Manager)]
        public ActionResult Index()
        {
            return this.View(this.skillLevelService.GetAll());
        }

        // GET: SkillLevel/Details/5
        [Roles(CustomRoles.Admin, CustomRoles.Manager)]
        public ActionResult Details(int id)
        {
            var category = this.skillLevelService.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            return this.View(category);
        }

        // GET: SkillLevel/Create
        [Roles(CustomRoles.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult Create(SkillLevelDto skillLevel)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.skillLevelService.Create(skillLevel);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.SkillLevelSuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(skillLevel);
        }

        // GET: SkillLevel/Edit/5
        [Roles(CustomRoles.Admin)]
        public ActionResult Edit(int id)
        {
            var model = this.skillLevelService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: SkillLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult Edit(SkillLevelDto skillLevel)
        {
            IServiceResult<SkillLevelDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.skillLevelService.Update(skillLevel);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.SkillLevelSuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(skillLevel);
        }

        // GET: SkillLevel/Delete/5
        [Roles(CustomRoles.Admin)]
        public ActionResult Delete(int id)
        {
            var model = this.skillLevelService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: SkillLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.skillLevelService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.SkillLevelSuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }
    }
}
