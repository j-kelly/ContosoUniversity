namespace ContosoUniversity.TestKit
{
    using NRepository.Core.Command;
    using NRepository.Core.Query;
    using NRepository.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    public class EfRepositoryTestExtension : IRepositoryExtensions
    {
        public int ExecuteSqlQueryCallCount { get; private set; }
        public int ExecuteStoredProcudureCallCount { get; private set; }

        public void AddRange<TEntity>(ICommandRepository commandRepository, IEnumerable<TEntity> entities) where TEntity : class
        {
        }

        public IEnumerable<T> ExecuteSqlQuery<T>(IQueryRepository repository, string sql, params object[] sqlParams)
        {
            ExecuteSqlQueryCallCount++;
            return new List<T>();
        }

        public int ExecuteStoredProcudure(ICommandRepository repository, string sql, params object[] sqlParams)
        {
            ExecuteStoredProcudureCallCount++;
            return 1;
        }

        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            throw new NotImplementedException();
        }

        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, IList<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            throw new NotImplementedException();
        }

        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, TElement>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            throw new NotImplementedException();
        }

        public void UpdateEntityState<TEntity>(ICommandRepository repository, TEntity entity, EntityState entityState) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
