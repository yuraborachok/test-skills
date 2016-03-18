namespace TC.SkillsDatabase.Web.Controllers.Base
{
    using System;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;

    public abstract class BaseSecurityAbstractController : BaseAbstractController
    {
        private ApplicationSignInManager signInManager;

        private ApplicationUserManager userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            protected set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }

            protected set
            {
                this.userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}