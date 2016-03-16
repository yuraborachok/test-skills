using Microsoft.Owin;
using TC.SkillsDatabase.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace TC.SkillsDatabase.Web
{
    using System;
    using BL;
    using LightInject;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            AutoMapperConfiguration.Configure();
            var container = InjectionConfig.RegisterAllDependencies();
            container.RegisterControllers();
            container.EnableMvc();
        }
    }
}
