namespace TC.SkillsDatabase.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Core.Results;

    public abstract class BaseAbstractController : Controller
    {
        public bool IsUserAdmin
        {
            get
            {
                return this.User.IsInRole("Admin");
            }
        }

        protected void ProcessNotifications(IServiceResult result)
        {
            if (result == null)
            {
                return;
            }

            if (result.Errors != null && result.Errors.Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    this.ProcessError(error.Message);
                }
            }

            if (result.Messages != null && result.Messages.Count > 0)
            {
                foreach (var message in result.Messages)
                {
                    this.ProcessMessage(message.Message);
                }
            }

            if (result.Warnings != null && result.Warnings.Count > 0)
            {
                foreach (var warning in result.Warnings)
                {
                    this.ProcessWarning(warning.Message);
                }
            }

            // TODO: re-implement notification processing to work with collections and new class
        }

        protected void ProcessError(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                return;
            }

            List<string> errors = null;
            if (this.TempData["Errors"] != null)
            {
                errors = this.TempData["Errors"] as List<string>;
            }

            if (errors == null)
            {
                errors = new List<string>();
            }

            if (!errors.Contains(errorMessage))
            {
                errors.Add(errorMessage);
            }

            this.TempData["Errors"] = errors;
        }

        protected void ProcessWarning(string warning)
        {
            if (string.IsNullOrWhiteSpace(warning))
            {
                return;
            }

            List<string> warnings = null;
            if (this.TempData["Warnings"] != null)
            {
                warnings = this.TempData["Warnings"] as List<string>;
            }

            if (warnings == null)
            {
                warnings = new List<string>();
            }

            if (!warnings.Contains(warning))
            {
                warnings.Add(warning);
            }

            this.TempData["Warnings"] = warnings;
        }

        protected void ProcessMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            List<string> messages = null;
            if (this.TempData["Messages"] != null)
            {
                messages = this.TempData["Messages"] as List<string>;
            }

            if (messages == null)
            {
                messages = new List<string>();
            }

            if (!messages.Contains(message))
            {
                messages.Add(message);
            }

            this.TempData["Messages"] = messages;
        }
    }
}