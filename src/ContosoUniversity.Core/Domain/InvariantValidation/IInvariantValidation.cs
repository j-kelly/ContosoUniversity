namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    public interface IInvariantValidation
    {
        void Assert(params object[] dependentServices);
    }
}
