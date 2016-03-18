namespace TC.SkillsDatabase.Web.Helpers
{
    using System;
    using System.Web.Mvc;

    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
        {
            this.Roles = string.Join(",", roles);
        }
    }
}