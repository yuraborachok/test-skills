namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using BL.Interfaces;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using Core.Utils;
    using ViewModels;

    public class SkillController : BaseAbstractController
    {
        private readonly ISkillService skillService;
        private readonly ICategoryService categoryService;

        public SkillController(ISkillService skillService, ICategoryService categoryService)
        {
            this.skillService = skillService;
            this.categoryService = categoryService;
        }

        // GET: Skill
        public ActionResult Index()
        {
            return this.View(this.skillService.GetAll());
        }

        // GET: Skill/Details/5
        public ActionResult Details(int id)
        {
            var category = this.skillService.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            return this.View(category);
        }

        // GET: Skill/Create
        public ActionResult Create()
        {
            return this.View(this.PopulateViewModel(new SkillDto()));
        }

        // POST: Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillDto skill)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.skillService.Create(skill);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.SkillSuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(this.PopulateViewModel(skill));
        }

        // GET: Skill/Edit/5
        public ActionResult Edit(int id)
        {
            var model = this.skillService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(this.PopulateViewModel(model));
        }

        // POST: Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillDto skill)
        {
            IServiceResult<SkillDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.skillService.Update(skill);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.SkillSuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(this.PopulateViewModel(skill));
        }

        // GET: Skill/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.skillService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.skillService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.SkillSuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }

        private SkillViewModel PopulateViewModel(SkillDto skillDto)
        {
            var skillViewModel = new SkillViewModel();
            skillDto.ShallowConvert(skillViewModel);
            skillViewModel.Categories = this.categoryService.GetAll();

            return skillViewModel;
        }
    }
}
