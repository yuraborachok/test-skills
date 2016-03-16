namespace TC.SkillsDatabase.BL
{
    using System;
    using DAL;
    using Interfaces;
    using LightInject;
    using Services;

    // using Services;

    public static class InjectionConfig
    {
        public static ServiceContainer RegisterAllDependencies()
        {
            var container = new ServiceContainer();

            container.Register<ITeamService, TeamService>(new PerScopeLifetime());
            container.Register<IResourceRoleService, ResourceRoleService>(new PerScopeLifetime());

            container.Register((serviceFactory) => new SkillsDatabaseContext(), new PerScopeLifetime());
            container.Register(typeof(IRepository<>), typeof(Repository<>));

            return container;
        }
    }
}
