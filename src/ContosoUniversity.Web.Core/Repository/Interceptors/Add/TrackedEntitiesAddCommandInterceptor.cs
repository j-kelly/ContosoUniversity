namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using Domain.Core.Repository;
    using NRepository.Core.Command;
    using System;

    public class TrackedEntitiesAddCommandInterceptor : IAddCommandInterceptor
    {
        public void Add<T>(ICommandRepository repository, Action<T> addAction, T entity) where T : class
        {
            if (typeof(ITrackedEntity).IsAssignableFrom(typeof(T)))
            {
                var trackedEntity = (ITrackedEntity)entity;
                trackedEntity.CreatedBy = CurrentPrincipalHelper.Name;
                trackedEntity.CreatedOn = DateTime.Now;
                trackedEntity.ModifiedBy = CurrentPrincipalHelper.Name;
                trackedEntity.ModifiedOn = DateTime.Now;
            }

            addAction.Invoke(entity);
        }
    }
}
