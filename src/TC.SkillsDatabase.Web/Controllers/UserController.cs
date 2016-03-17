namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Base;
    using Core.Models.DbModels;
    using Core.Properties;
    using Microsoft.AspNet.Identity;
    using Models;

    public class UserController : BaseSecurityAbstractController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            return this.View(this.db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.db.Users.Find(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            return this.View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var newUser = new User { UserName = user.Email, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, EmailConfirmed = true, LockoutEnabled = false };

                newUser.Claims.Add(new CustomUserClaim { ClaimType = ClaimTypes.GivenName, ClaimValue = user.FirstName });

                var result = await this.UserManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    result = await this.UserManager.AddToRoleAsync(newUser.Id, "User");
                    if (result.Succeeded)
                    {
                        this.ProcessMessage(Resources.UserSuccesfullyCreated);

                        return this.RedirectToAction("Index");
                    }
                }

                this.AddErrors(result);
            }

            return this.View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.db.Users.Find(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            return this.View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (this.ModelState.IsValid)
            {
                // this.db.Entry(user).State = EntityState.Modified;
                var originalUser = this.db.Users.FirstOrDefault(u => u.Id == user.Id);
                if (originalUser == null)
                {
                    return this.HttpNotFound();
                }

                if (!originalUser.FirstName.Equals(user.FirstName))
                {
                    originalUser.FirstName = user.FirstName;

                    // Update claim for FirstName
                    var claims = this.UserManager.GetClaims(user.Id);
                    foreach (var claim in claims.Where(claim => claim.Type == ClaimTypes.GivenName))
                    {
                        this.UserManager.RemoveClaim(user.Id, claim);
                    }

                    this.UserManager.AddClaim(user.Id, new Claim(ClaimTypes.GivenName, user.FirstName));
                }

                originalUser.LastName = user.LastName;

                if (!originalUser.Email.Equals(user.Email))
                {
                    originalUser.Email = user.Email;
                    originalUser.UserName = user.Email;
                }

                originalUser.EmailConfirmed = user.EmailConfirmed;

                this.db.SaveChanges();

                this.ProcessMessage(Resources.UserSuccesfullyUpdated);

                return this.RedirectToAction("Index");
            }

            return this.View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.db.Users.Find(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            return this.View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = this.db.Users.Find(id);
            this.db.Users.Remove(user);
            this.db.SaveChanges();

            this.ProcessMessage(Resources.UserSuccesfullyDeleted);

            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}
