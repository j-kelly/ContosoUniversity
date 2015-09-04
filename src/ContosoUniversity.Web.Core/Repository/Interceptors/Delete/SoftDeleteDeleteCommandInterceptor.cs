namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using Domain.Core.Repository;
    using NRepository.Core.Command;
    using System;

    public class SoftDeleteDeleteCommandInterceptor : IDeleteCommandInterceptor
    {
        public void Delete<T>(ICommandRepository repository, Action<T> deleteAction, T entity) where T : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                // modify it instead of deleting it
                ((ISoftDelete)entity).IsDeleted = true;
                repository.Modify(entity);
                return;
            };

            // Could remove foreign key constrained objects here
            // var queryRepository = new EntityFrameworkQueryRepository((DbContext)repository.ObjectContext);

            // Delete as normal
            deleteAction(entity);
        }
    }
}
