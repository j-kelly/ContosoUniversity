namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using Domain.Core.Repository;
    using NRepository.Core.Command;
    using System;

    public class TrackedEntitiesAddCommandInterceptor : IAddCommandInterceptor
    {
        public void Add<T>(ICommandRepository repository, Action<T> addAction, T entity) where T : class
        {
            if (entity is ITrackedEntity)
            {
                var trackedEntity = (ITrackedEntity)entity;
                trackedEntity.SetCreateAndModifiedFields(SystemPrincipal.Name);
            }

            addAction.Invoke(entity);
        }
    }
}
