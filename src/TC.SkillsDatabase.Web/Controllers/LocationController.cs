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

    public class LocationController : BaseAbstractController
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        // GET: Location
        public ActionResult Index()
        {
            return View(this.locationService.GetAll());
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            var location = this.locationService.GetById(id);

            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationDto location)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.locationService.Create(location);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.LocationSuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(location);
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            var location = this.locationService.GetById(id);
            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationDto location)
        {
            IServiceResult<LocationDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.locationService.Update(location);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.LocationSuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(location);
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.locationService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.locationService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.LocationSuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }
    }
}
