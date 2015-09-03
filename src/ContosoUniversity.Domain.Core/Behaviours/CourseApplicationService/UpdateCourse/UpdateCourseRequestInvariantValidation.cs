namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class UpdateCourseRequestInvariantValidation : UserDomainRequestInvariantValidation<UpdateCourseRequest, UpdateCourseCommandModel>
    {
        public UpdateCourseRequestInvariantValidation(UpdateCourseRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }

        public void CourseIdCannotBeLessThan1()
        {
            Assert(Context.CommandModel.CourseID > 0);
        }

        public void DepartmentIDCannotBeLessThan1()
        {
            Assert(Context.CommandModel.DepartmentID > 0);
        }
    }
}
