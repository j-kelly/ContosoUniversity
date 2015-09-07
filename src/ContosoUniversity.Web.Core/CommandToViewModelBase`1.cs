namespace ContosoUniversity.Web.Core
{
    public class CommandToViewModelBase<T> where T : class, new()
    {
        public CommandToViewModelBase()
        {
            CommandModel = new T();
        }

        public CommandToViewModelBase(T commandModel)
        {
            CommandModel = commandModel;
        }

        public T CommandModel { get; }
    }
}
