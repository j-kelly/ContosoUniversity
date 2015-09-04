namespace ContosoUniversity.Domain.Core.Repository
{
    using System;

    public interface ITrackedEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
