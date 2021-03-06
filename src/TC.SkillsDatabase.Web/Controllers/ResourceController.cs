﻿namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using BL.Interfaces;
    using Core;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;
    using Core.Utils;
    using Helpers;
    using ViewModels;

    public class ResourceController : BaseAbstractController
    {
        private readonly IResourceService resourceService;
        private readonly ITeamService teamService;
        private readonly ILocationService locationService;
        private readonly IResourceRoleService resourceRoleService;

        public ResourceController(IResourceService resourceService, ITeamService teamService, ILocationService locationService, IResourceRoleService resourceRoleService)
        {
            this.resourceService = resourceService;
            this.teamService = teamService;
            this.locationService = locationService;
            this.resourceRoleService = resourceRoleService;
        }

        // GET: Resource
        [Roles(CustomRoles.Admin, CustomRoles.Manager)]
        public ActionResult Index(string resourceSearchText)
        {
            return this.View(new ResourceSearchViewModel() { ResourceSearchText  = resourceSearchText, Resources = this.resourceService.GetAll(resourceSearchText).ToList() });
        }

        // GET: Resource/Details/5
        [Roles(CustomRoles.Admin, CustomRoles.Manager)]
        public ActionResult Details(int id)
        {
            var resource = this.resourceService.GetById(id);
            if (resource == null)
            {
                return this.HttpNotFound();
            }

            return this.View(resource);
        }

        // GET: Resource/Create
        [Roles(CustomRoles.Admin)]
        public ActionResult Create()
        {
            return this.View(this.PopulateViewModel(new ResourceDto()));
        }

        // POST: Resource/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult Create(ResourceDto resource)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.resourceService.Create(resource);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.ResourceSuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(this.PopulateViewModel(resource));
        }

        // GET: Resource/Edit/5
        [Roles(CustomRoles.Admin)]
        public ActionResult Edit(int id)
        {
            var model = this.resourceService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(this.PopulateViewModel(model));
        }

        // POST: Resource/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult Edit(ResourceDto resource)
        {
            IServiceResult<ResourceDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.resourceService.Update(resource);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.ResourceSuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(this.PopulateViewModel(resource));
        }

        // GET: Resource/Delete/5
        [Roles(CustomRoles.Admin)]
        public ActionResult Delete(int id)
        {
            var model = this.resourceService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Resource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Roles(CustomRoles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.resourceService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.ResourceSuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }

        private ResourceViewModel PopulateViewModel(ResourceDto resourceDto)
        {
            var resourceViewModel = new ResourceViewModel();
            resourceDto.ShallowConvert(resourceViewModel);
            resourceViewModel.Teams = this.teamService.GetAll();
            resourceViewModel.ResourceRoles = this.resourceRoleService.GetAll();
            resourceViewModel.Locations = this.locationService.GetAll();

            return resourceViewModel;
        }
    }
}
