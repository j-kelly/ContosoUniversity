namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using Audit;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using NRepository.Core;
    using NRepository.Core.Command;
    using System.Collections.Generic;
    public class ContosoUniversitySaveCommandInterceptor : AuditSaveCommandInterceptorBase
    {
        public override bool ThrowOriginalException { get; } = true;

        protected override IEnumerable<IAuditItem> GetAuditItems(ICommandRepository commandRepository)
        {
            // Set up monitoring on specific columns (Add change information to the AuditPropertyTrails table).
            return new[]
            {
                new AuditItem(
                    typeof(Instructor), 
                    // new AuditPropertyItem(PropertyInfo<Instructor>.GetMemberName(p => p.FirstMidName), p => ((string)p).ToLower()),
                    new AuditPropertyItem(PropertyInfo<Instructor>.GetMemberName(p => p.FirstMidName)),
                    new AuditPropertyItem(PropertyInfo<Instructor>.GetMemberName(p => p.LastName))),
            };
        }
    }
}
