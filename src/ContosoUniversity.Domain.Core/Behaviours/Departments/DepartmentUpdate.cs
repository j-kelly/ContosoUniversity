﻿namespace ContosoUniversity.Domain.Core.Behaviours.Departments
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DepartmentUpdate
    {
        // DepartmentUpdate.CommandModel
        public class CommandModel : ICommandModel
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

        // DepartmentUpdate.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // DepartmentUpdate.Response
        public class Response : DomainResponse
        {
            // If you are using the auto-validation 'decorator' do not change the signiture of this ctor (see AutoValidate<T>) in the 
            // DomainBootstrapper class
            public Response(ValidationMessageCollection validationDetails)
                : base(validationDetails)
            {
            }

            public Response(ValidationMessageCollection validationDetails, byte[] rowVersion)
            : base(validationDetails)
            {
                RowVersion = rowVersion;
            }

            public byte[] RowVersion
            {
                get;
            }
        }

        // DepartmentUpdate.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // DepartmentUpdate.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }

            public override void ValidateContext()
            {
                ValidateOneAdministratorAssignmentPerInstructor();
            }

            private void ValidateOneAdministratorAssignmentPerInstructor()
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

                    ValidationMessageCollection.Add(string.Empty, errorMessage);
                }
            }
        }
    }
}
