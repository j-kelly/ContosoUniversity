namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService
{

    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using Models;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateDepartment
    {
        // UpdateDepartment.CommandModel
        public class CommandModel
        {
            public int DepartmentID { get; set; }

            [StringLength(50, MinimumLength = 3)]
            public string Name { get; set; }

            [DataType(DataType.Currency)]
            public decimal Budget { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Display(Name = "Start Date")]
            public DateTime StartDate { get; set; }

            public int? InstructorID { get; set; }

            public byte[] RowVersion { get; set; }
        }

        // UpdateDepartment.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // UpdateDepartment.Response
        public class Response : DomainResponse
        {
            public Response(ValidationMessageCollection validationDetails, byte[] rowVersion = null)
            : base(validationDetails)
            {
                RowVersion = rowVersion;
            }

            public byte[] RowVersion
            {
                get;
            }
        }

        // UpdateDepartment.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // UpdateDepartment.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }

            public override void Validate(ValidationMessageCollection validationMessages)
            {
                ValidateOneAdministratorAssignmentPerInstructor(validationMessages);
            }

            private void ValidateOneAdministratorAssignmentPerInstructor(ValidationMessageCollection validationMessages)
            {
                if (Context.CommandModel.InstructorID == null)
                    return;

                var queryRepository = ResolveService<IQueryRepository>();
                var duplicateDepartment = queryRepository.GetEntity<Department>(
                    p => p.InstructorID == Context.CommandModel.InstructorID.Value,
                    new AsNoTrackingQueryStrategy(),
                    new EagerLoadingQueryStrategy<Department>(p => p.Administrator),
                    false);

                if (duplicateDepartment != null && duplicateDepartment.DepartmentID != Context.CommandModel.DepartmentID)
                {
                    string errorMessage =
                        $"Instructor {duplicateDepartment.Administrator.FirstMidName} {duplicateDepartment.Administrator.LastName} " +
                        $"is already administrator of the {duplicateDepartment.Name} department.";

                    validationMessages.Add(string.Empty, errorMessage);
                }
            }
        }
    }
}
