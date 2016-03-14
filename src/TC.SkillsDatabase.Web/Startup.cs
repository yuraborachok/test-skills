using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TC.SkillsDatabase.Web.Startup))]
namespace TC.SkillsDatabase.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
