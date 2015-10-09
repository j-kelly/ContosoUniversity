namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    public class UserDomainRequestInvariantValidation<T, TCommandModel> : InvariantValidation<T> 
        where T : DomainRequest<TCommandModel> 
        where TCommandModel : class, ICommandModel
     
    {
        public UserDomainRequestInvariantValidation(T context)
            : base(context)
        {
        }

        public override void ValidateContext()
        {
            base.ValidateContext();

            Assert(!string.IsNullOrWhiteSpace(Context.UserId), "UserId cannot be null or whitespace");
        }
    }
}
