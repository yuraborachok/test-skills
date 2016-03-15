namespace TC.SkillsDatabase.BL
{
    using System;
    using DAL;
    using LightInject;
    // using Interfaces;

    // using Services;

    public static class InjectionConfig
    {
        public static ServiceContainer RegisterAllDependencies()
        {
            var container = new ServiceContainer();

            // container.Register<ICategoryService, CategoryService>(new PerScopeLifetime());

            container.Register((serviceFactory) => new SkillsDatabaseContext(), new PerScopeLifetime());
            container.Register(typeof(IRepository<>), typeof(Repository<>));

            return container;
        }
    }
}
