namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using BL.Interfaces;
    using Core.Models.DbModels;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using DAL;

    public class CategoryController : BaseAbstractController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: Category
        public ActionResult Index()
        {
            return this.View(this.categoryService.GetAll());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var category = this.categoryService.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            return this.View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDto category)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.categoryService.Create(category);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.CategorySuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var model = this.categoryService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryDto category)
        {
            IServiceResult<CategoryDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.categoryService.Update(category);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.CategorySuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.categoryService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.categoryService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.CategorySuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }
    }
}
