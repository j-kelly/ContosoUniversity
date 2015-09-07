namespace ContosoUniversity.Domain.Core.Repository.Containers
{
    public class EntityStateWrapper
    {
        public static EntityStateWrapper<T> Add<T>(T entity) where T : class
        {
            return new EntityStateWrapper<T>(State.Added, entity);
        }

        public static EntityStateWrapper<T> Modify<T>(T entity) where T : class
        {
            return new EntityStateWrapper<T>(State.Modified, entity);
        }

        public static EntityStateWrapper<T> Delete<T>(T entity) where T : class
        {
            return new EntityStateWrapper<T>(State.Deleted, entity);
        }
    }
}