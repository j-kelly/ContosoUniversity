namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Person : IEntity
    {
        public int ID { get; internal set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; internal set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; internal set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{LastName}, {FirstMidName}";

        public void SetID(int id)
        {
            // assert id > 0
            ID = id;
        }
    }
}