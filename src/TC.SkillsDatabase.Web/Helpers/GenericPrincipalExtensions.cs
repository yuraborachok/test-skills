namespace TC.SkillsDatabase.Web.Helpers
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    public static class GenericPrincipalExtensions
    {
        public static string FirstName(this IPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                var claim = ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName);

                return claim != null
                           ? claim.Value
                           : string.Empty;
            }

            return string.Empty;
        }
    }
}