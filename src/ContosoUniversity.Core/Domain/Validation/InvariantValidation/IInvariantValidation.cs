namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    public interface IInvariantValidation
    {
        void Validate(params object[] dependentServices);
    }
}
