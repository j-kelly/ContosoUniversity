namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using ContosoUniversity.Core.Utilities;
    using ContosoUniversity.Domain.Core.Repository;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class TrackedEntity : ITrackedEntity
    {
        [Required]
        public string CreatedBy { get; internal set; }

        [Required]
        public string ModifiedBy { get; internal set; }

        public DateTime CreatedOn { get; internal set; }

        public DateTime ModifiedOn { get; internal set; }

        public void SetCreateAndModifiedFields(string name)
        {
            Check.NotNull(name, nameof(name));

            CreatedOn = DateTimeHelper.Now;
            CreatedBy = name;

            SetModifedFields(name);
        }
        public void SetModifedFields(string name)
        {
            Check.NotNull(name, nameof(name));

            ModifiedBy = name;
            ModifiedOn = DateTimeHelper.Now;
        }
    }
}
