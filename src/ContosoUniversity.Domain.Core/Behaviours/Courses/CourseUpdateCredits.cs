﻿namespace ContosoUniversity.Domain.Core.Behaviours.Courses
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class CourseUpdateCredits
    {
        // CourseUpdateCredits.CommandModel
        public class CommandModel : ICommandModel
        {
            public int Multiplier { get; set; }
        }

        // CourseUpdateCredits.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // CourseUpdateCredits.Response
        public class Response : DomainResponse
        {
            public Response(int rowsEffected)
            {
                RowsEffected = rowsEffected;
            }

            public Response(ValidationMessageCollection validationDetails)
                : base(validationDetails)
            {
            }

            public int? RowsEffected { get; }
        }

        // CourseUpdateCredits.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // CourseUpdateCredits.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
