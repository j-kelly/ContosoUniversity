namespace NRepository.EntityFramework
{
    using NRepository.Core.Command;
    using NRepository.Core.Events;
    using NRepository.Core.Query;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;

    public class DummyEntityFrameworkRepositoryExtensions : IRepositoryExtensions
    {
        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, TElement>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
        }

        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
        }

        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, IList<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
        }

        public void UpdateEntityState<TEntity>(ICommandRepository repository, TEntity entity, EntityState entityState) where TEntity : class
        {
            // Raise any events for testing
            switch (entityState)
            {
                case EntityState.Added:
                    repository.RaiseEvent(new EntityAddedEvent(repository, entity));
                    break;
                case EntityState.Deleted:
                    repository.RaiseEvent(new EntityDeletedEvent(repository, entity));
                    break;
                case EntityState.Modified:
                    repository.RaiseEvent(new EntityModifiedEvent(repository, entity));
                    break;
            }
        }

        public IEnumerable<T> ExecuteSqlQuery<T>(IQueryRepository repository, string sql, params object[] sqlParams)
        {
            return default(IEnumerable<T>);
        }

        public int ExecuteStoredProcudure(ICommandRepository repository, string sql, params object[] sqlParams)
        {
            return 0;
        }

        public void AddRange<TEntity>(ICommandRepository commandRepository, IEnumerable<TEntity> entities) where TEntity : class
        {
        }
    }
}
