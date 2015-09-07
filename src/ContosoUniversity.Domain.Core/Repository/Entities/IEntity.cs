namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    // here purely for audit purposes
    public interface IEntity
    {
        int ID { get; }

        void SetID(int id);
    }
}
