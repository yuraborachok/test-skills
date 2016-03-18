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

    public class ResourceRoleController : BaseAbstractController
    {
        private readonly IResourceRoleService resourceRoleService;

        public ResourceRoleController(IResourceRoleService resourceRoleService)
        {
            this.resourceRoleService = resourceRoleService;
        }

        // GET: ResourceRole
        [Roles(CustomRoles.Admin, CustomRoles.Manager)]
        public ActionResult Index()
        {
            return this.View(this.resourceRoleService.GetAll());
        }

        // GET: ResourceRole/Details/5
        [Roles(CustomRoles.Admin, CustomRoles.Manager)]
        public ActionResult Details(int id)
        {
            var category = this.resourceRoleService.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            return this.View(category);
        }

        // GET: ResourceRole/Create
        [Roles(CustomRoles.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResourceRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult Create(ResourceRoleDto resourceRole)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.resourceRoleService.Create(resourceRole);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.ResourceRoleSuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(resourceRole);
        }

        // GET: ResourceRole/Edit/5
        [Roles(CustomRoles.Admin)]
        public ActionResult Edit(int id)
        {
            var model = this.resourceRoleService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: ResourceRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult Edit(ResourceRoleDto resourceRole)
        {
            IServiceResult<ResourceRoleDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.resourceRoleService.Update(resourceRole);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.ResourceRoleSuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(resourceRole);
        }

        // GET: ResourceRole/Delete/5
        [Roles(CustomRoles.Admin)]
        public ActionResult Delete(int id)
        {
            var model = this.resourceRoleService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: ResourceRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.resourceRoleService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.ResourceRoleSuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }
    }
}
