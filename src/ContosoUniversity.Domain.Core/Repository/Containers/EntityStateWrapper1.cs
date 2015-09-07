namespace ContosoUniversity.Domain.Core.Repository.Containers
{
    using System;

    public class EntityStateWrapper<TEntity> : IState where TEntity : class
    {
        public EntityStateWrapper(State state, TEntity entity)
        {
            State = state;
            Entity = entity;
            BatchId = int.MaxValue;
        }

        public TEntity Entity
        {
            get;
        }

        public State State
        {
            get;
            set;
        }

        public int BatchId
        {
            get;
            set;
        }

        public static implicit operator TEntity(EntityStateWrapper<TEntity> wrapper)
        {
            return wrapper.Entity;
        }

        internal object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
