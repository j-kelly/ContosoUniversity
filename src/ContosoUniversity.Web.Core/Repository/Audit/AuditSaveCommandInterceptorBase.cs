namespace ContosoUniversity.Web.Core.Repository.Audit
{
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Models;
    using NRepository.Core.Command;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public abstract class AuditSaveCommandInterceptorBase : ISaveCommandInterceptor
    {
        private IEnumerable<IAuditItem> auditItems;

        public abstract bool ThrowOriginalException { get; }

        public virtual int Save(ICommandRepository commandRepository, Func<int> saveFunc)
        {
            auditItems = GetAuditItems(commandRepository);

            var dbContext = commandRepository.ObjectContext as DbContext;
            if (dbContext == null)
                throw new NotSupportedException("AuditSaveCommand.Save can only be used with a DBContext");

            // Modified Items
            var modifiedItems = dbContext.ChangeTracker.Entries().Where(entity => entity != null && entity.State == EntityState.Modified);
            if (modifiedItems.Any())
                AuditAnyRequiredChanges(commandRepository, modifiedItems, dbContext);

            return saveFunc.Invoke();
        }

        protected abstract IEnumerable<IAuditItem> GetAuditItems(ICommandRepository commandRepository);

        private void AuditAnyRequiredChanges(ICommandRepository repository, IEnumerable<DbEntityEntry> modifiedItems, DbContext dbContext)
        {
            var manager = ((IObjectContextAdapter)dbContext).ObjectContext.ObjectStateManager;
            var relations = manager.GetObjectStateEntries(EntityState.Deleted).Where(p => p.IsRelationship);

            // The entityType could be either a normal type of proxy wrapper therefore we need to use GetObjectType.
            var auditableEntities = modifiedItems.Where(p => auditItems.Select(p1 => p1.ClassType).Contains(ObjectContext.GetObjectType(p.Entity.GetType())));
            foreach (var context in auditableEntities)
            {
                // Check each property for changes
                var BluePearEntityType = ObjectContext.GetObjectType(context.Entity.GetType());
                var auditItem = auditItems.Single(p => p.ClassType == BluePearEntityType);
                auditItem.AuditPropertyItems.ToList().ForEach(auditPropertyItem =>
                {
                    var oldValue = default(object);
                    var currentValue = default(object);
                    if (!auditPropertyItem.IsRelationship)
                    {
                        var olddbValue = context.OriginalValues.GetValue<object>(auditPropertyItem.PropertyName);
                        var newDbValue = auditPropertyItem.PropertyInfo.GetValue(context.Entity);

                        // Transform them if required
                        oldValue = auditPropertyItem.GetValue(olddbValue);
                        currentValue = auditPropertyItem.GetValue(newDbValue);
                    }
                    else
                    {
                        foreach (var deletedEntity in relations.Where(p => p.EntitySet.Name == BluePearEntityType.Name + "_" + auditPropertyItem.PropertyName))
                        {
                            var entityId = ((EntityKey)deletedEntity.OriginalValues[0]).EntityKeyValues[0].Value;
                            if ((int)entityId == ((IEntity)context.Entity).ID)
                            {
                                oldValue = ((EntityKey)deletedEntity.OriginalValues[1]).EntityKeyValues[0].Value;
                                break;
                            }
                        }

                        // if its null it hasn't changed
                        if (oldValue == null)
                            return;

                        currentValue = ((IEntity)context.Member(auditPropertyItem.PropertyName).CurrentValue).ID;
                    }

                    if (!currentValue.Equals(oldValue))
                    {
                        // Add the audit!
                        var item = new AuditPropertyTrail();
                        item.EntityType = BluePearEntityType.Name;
                        item.EntityId = ((IEntity)context.Entity).ID;
                        item.PropertyName = auditPropertyItem.PropertyName;
                        item.NewValue = currentValue.ToString();
                        item.OldValue = oldValue.ToString();

                        repository.Add(item);
                    }
                });
            }
        }
    }
}