namespace ContosoUniversity.Domain.Core.Repository
{
    using System;

    public interface ITrackedEntity
    {
        string CreatedBy { get; }
        DateTime CreatedOn { get; }
        string ModifiedBy { get; }
        DateTime ModifiedOn { get; }

        void SetModifedFields(string name);
        void SetCreateAndModifiedFields(string name);
    }
}
