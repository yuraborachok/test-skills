namespace TC.SkillsDatabase.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly SkillsDatabaseContext skillsDatabaseContext;
        private readonly DbSet<T> dataSet;

        public Repository(SkillsDatabaseContext skillsDatabaseContext)
        {
            this.skillsDatabaseContext = skillsDatabaseContext;
            this.dataSet = skillsDatabaseContext.GetPropertyByType<T>();
        }

        public IQueryable<T> GetAll()
        {
            return dataSet;
        }

        public IQueryable<T> GetPage(int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, out int totalCount)
        {
            totalCount = dataSet.Count();

            return orderBy(dataSet).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<T> GetPage(int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, bool>> searchExpression, out int totalCount)
        {
            var data = dataSet.Where(searchExpression);
            totalCount = data.Count();

            return orderBy(data).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Insert(T entity)
        {
            dataSet.Add(entity);
            this.skillsDatabaseContext.SaveChanges();
        }

        public void Update(T entity)
        {
            this.skillsDatabaseContext.Entry(entity).State = EntityState.Modified;
            this.skillsDatabaseContext.SaveChanges();
        }

        public void Update(T entity, params Expression<Func<T, object>>[] properties)
        {
            DetachEntity(entity);
            this.skillsDatabaseContext.Set<T>().Attach(entity);

            var entry = this.skillsDatabaseContext.Entry(entity);

            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }

            this.skillsDatabaseContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.skillsDatabaseContext.Entry(entity).State = EntityState.Deleted;
            this.skillsDatabaseContext.SaveChanges();
        }

        public void SaveChanges()
        {
            this.skillsDatabaseContext.SaveChanges();
        }

        public void TruncateTable(string tableName)
        {
            this.skillsDatabaseContext.Database.ExecuteSqlCommand(string.Format("TRUNCATE TABLE [{0}]", tableName));
        }

        private void DetachEntity(T entity)
        {
            var objContext = ((IObjectContextAdapter)this.skillsDatabaseContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // TryGetObjectByKey attaches a found entity
            // Detach it here to prevent side-effects
            if (exists)
            {
                objContext.Detach(foundEntity);
            }
        }
    }
}
