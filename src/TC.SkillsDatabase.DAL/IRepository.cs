namespace TC.SkillsDatabase.DAL
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetPage(int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, out int totalCount);

        IQueryable<T> GetPage(int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, bool>> searchExpression, out int totalCount);

        void Insert(T entity);

        void Update(T entity);

        void Update(T entity, params Expression<Func<T, object>>[] properties);

        void Delete(T entity);

        void TruncateTable(string tableName);

        void SaveChanges();
    }
}
