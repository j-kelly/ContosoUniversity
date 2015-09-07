namespace ContosoUniversity.Domain.Core.Factories
{
    using Behaviours.DepartmentApplicationService;
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Repository.Containers;

    public static class DepartmentFactory
    {
        public static Department CreatePartial(int departmentId)
        {
            return new Department { DepartmentID = departmentId };
        }

        public static EntityStateWrapperContainer Create(CreateDepartment.CommandModel commandModel)
        {
            var dept = new Department
            {
                Budget = commandModel.Budget,
                InstructorID = commandModel.InstructorID,
                Name = commandModel.Name,
                StartDate = commandModel.StartDate,
            };

            return new EntityStateWrapperContainer().AddEntity(dept);
        }
    }
}