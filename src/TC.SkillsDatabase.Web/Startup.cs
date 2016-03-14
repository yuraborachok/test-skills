using Microsoft.Owin;
using TC.SkillsDatabase.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace TC.SkillsDatabase.Web
{
    using System;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
