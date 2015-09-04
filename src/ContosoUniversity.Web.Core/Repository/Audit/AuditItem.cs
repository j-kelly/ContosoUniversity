namespace ContosoUniversity.Web.Core.Repository.Audit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AuditItem : ContosoUniversity.Web.Core.Repository.Audit.IAuditItem
    {
        public AuditItem(Type classType, params string[] isModified)
        {
            ClassType = classType;

            var auditItems = new List<ContosoUniversity.Web.Core.Repository.Audit.AuditPropertyItem>();
            isModified.ToList().ForEach(propertyName =>
            {
                var item = new ContosoUniversity.Web.Core.Repository.Audit.AuditPropertyItem(propertyName, false);
                item.SetPropertyInfo(ClassType);
                auditItems.Add(item);
            });

            AuditPropertyItems = auditItems;
        }

        public AuditItem(Type classType, params ContosoUniversity.Web.Core.Repository.Audit.AuditPropertyItem[] propertyItems)
        {
            ClassType = classType;

            var propInfos = new List<ContosoUniversity.Web.Core.Repository.Audit.AuditPropertyItem>();
            propertyItems.ToList().ForEach(item =>
            {
                item.SetPropertyInfo(ClassType);
                propInfos.Add(item);
            });

            AuditPropertyItems = propInfos;
        }

        public IEnumerable<ContosoUniversity.Web.Core.Repository.Audit.AuditPropertyItem> AuditPropertyItems
        {
            get;
            private set;
        }

        public Type ClassType
        {
            get;
            private set;
        }
    }
}
