namespace ContosoUniversity.Web.Automation.Tests.Scaffolding
{
    public enum EntityType
    {
        Department,
        Course,
        Instructor,
        Student
    }

    public class AddedEntityInfo
    {
        public AddedEntityInfo(EntityType type, object id)
        {
            Type = type;
            Id = id;
        }

        public object Id { get; }
        public EntityType Type { get; }
    }
}
