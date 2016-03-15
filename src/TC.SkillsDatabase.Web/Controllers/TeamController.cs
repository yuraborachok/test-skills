namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using BL.Interfaces;
    using Core.Models.DTO;
    using Core.Properties;
    using Core.Results;

    public class TeamController : BaseAbstractController
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        // GET: Team
        public ActionResult Index()
        {
            return this.View(this.teamService.GetAll());
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var category = this.teamService.GetById(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }

            return this.View(category);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamDto team)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.teamService.Create(team);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.TeamSuccesfullyCreated);
                    return this.RedirectToAction("Index");
                }

                this.ProcessNotifications(result);
            }

            return this.View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            var model = this.teamService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamDto team)
        {
            IServiceResult<TeamDto> result = null;
            if (this.ModelState.IsValid)
            {
                result = this.teamService.Update(team);

                if (result.IsValid)
                {
                    this.ProcessMessage(Resources.TeamSuccesfullyUpdated);
                    return this.RedirectToAction("Index");
                }
            }

            this.ProcessNotifications(result);

            return this.View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.teamService.GetById(id);
            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = this.teamService.Delete(id);

            if (result)
            {
                this.ProcessMessage(Resources.TeamSuccesfullyDeleted);
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Delete", new { id });
        }
    }
}
