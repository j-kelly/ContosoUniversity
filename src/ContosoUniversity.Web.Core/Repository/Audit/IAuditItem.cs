namespace ContosoUniversity.Web.Core.Repository.Audit
{
    using System;
    using System.Collections.Generic;

    public interface IAuditItem
    {
        IEnumerable<AuditPropertyItem> AuditPropertyItems { get; }
        Type ClassType { get; }
    }
}
