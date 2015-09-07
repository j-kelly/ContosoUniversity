namespace ContosoUniversity.Web.Core.Repository.Audit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AuditItem : IAuditItem
    {
        public AuditItem(Type classType, params string[] isModified)
        {
            ClassType = classType;

            var auditItems = new List<AuditPropertyItem>();
            isModified.ToList().ForEach(propertyName =>
            {
                var item = new AuditPropertyItem(propertyName, false);
                item.SetPropertyInfo(ClassType);
                auditItems.Add(item);
            });

            AuditPropertyItems = auditItems;
        }

        public AuditItem(Type classType, params AuditPropertyItem[] propertyItems)
        {
            ClassType = classType;

            var propInfos = new List<AuditPropertyItem>();
            propertyItems.ToList().ForEach(item =>
            {
                item.SetPropertyInfo(ClassType);
                propInfos.Add(item);
            });

            AuditPropertyItems = propInfos;
        }

        public IEnumerable<AuditPropertyItem> AuditPropertyItems
        {
            get;
        }

        public Type ClassType
        {
            get;
        }
    }
}
