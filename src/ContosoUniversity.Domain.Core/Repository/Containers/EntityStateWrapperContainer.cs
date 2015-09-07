namespace ContosoUniversity.Domain.Core.Repository.Containers
{
    using System.Collections.Generic;
    using System.Linq;

    public class EntityStateWrapperContainer
    {
        private readonly List<EntityStateWrapper<object>> _Entities = new List<EntityStateWrapper<object>>();

        public IEnumerable<EntityStateWrapper<object>> Entities
        {
            get { return _Entities; }
        }

        public IEnumerable<IEnumerable<EntityStateWrapper<object>>> UnitsOfWork()
        {
            var uow = from entity in Entities
                      group entity by entity.BatchId into batchedGroup
                      orderby batchedGroup.Key
                      select batchedGroup;

            return uow;
        }

        public EntityStateWrapperContainer AttachEntity(object entity, int batchId = int.MaxValue)
        {
            var wrapper = new EntityStateWrapper<object>(State.Unchanged, entity)
            {
                BatchId = batchId
            };

            _Entities.Add(wrapper);
            return this;
        }

        public EntityStateWrapperContainer AddEntity(object entity, int batchId = int.MaxValue)
        {
            var wrapper = new EntityStateWrapper<object>(State.Added, entity)
            {
                BatchId = batchId
            };

            _Entities.Add(wrapper);
            return this;
        }

        public EntityStateWrapperContainer DeleteEntity(object entity, int batchId = int.MaxValue)
        {
            var wrapper = new EntityStateWrapper<object>(State.Deleted, entity)
            {
                BatchId = batchId
            };

            _Entities.Add(wrapper);
            return this;
        }

        public EntityStateWrapperContainer ModifyEntity(object entity, int batchId = int.MaxValue)
        {
            var wrapper = new EntityStateWrapper<object>(State.Modified, entity)
            {
                BatchId = batchId
            };

            _Entities.Add(wrapper);
            return this;
        }

        public void AddEntityStateWrapper(EntityStateWrapper<object> entityWrapper)
        {
            _Entities.Add(entityWrapper);
        }

        public void Add(EntityStateWrapperContainer container)
        {
            _Entities.AddRange(container.Entities);
        }
    }
}
