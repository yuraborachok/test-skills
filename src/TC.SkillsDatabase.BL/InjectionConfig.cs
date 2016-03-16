namespace TC.SkillsDatabase.BL
{
    using System;
    using DAL;
    using Interfaces;
    using LightInject;
    using Services;

    public static class InjectionConfig
    {
        public static ServiceContainer RegisterAllDependencies()
        {
            var container = new ServiceContainer();

            container.Register<ITeamService, TeamService>(new PerScopeLifetime());
            container.Register<ILocationService, LocationService>(new PerScopeLifetime());
            container.Register<IResourceRoleService, ResourceRoleService>(new PerScopeLifetime());
            container.Register<ICategoryService, CategoryService>(new PerScopeLifetime());
            container.Register<ISkillService, SkillService>(new PerScopeLifetime());
            container.Register<ISkillLevelService, SkillLevelService>(new PerScopeLifetime());
            container.Register<IResourceSkillService, ResourceSkillService>(new PerScopeLifetime());
            container.Register<IResourceService, ResourceService>(new PerScopeLifetime());

            container.Register((serviceFactory) => new SkillsDatabaseContext(), new PerScopeLifetime());
            container.Register(typeof(IRepository<>), typeof(Repository<>));

            return container;
        }
    }
}
