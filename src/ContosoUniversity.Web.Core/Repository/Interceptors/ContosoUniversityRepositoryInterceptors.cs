namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using NRepository.Core;

    // Add intercepts in this class for Add, Delete, Modify, Save & query
    public class ContosoUniversityRepositoryInterceptors : RepositoryInterceptors
    {
        public ContosoUniversityRepositoryInterceptors()
        {
            // base.QueryInterceptor = new ContosoFactoryQueryInterceptor();
            SaveCommandInterceptor = new ThrowOriginalExceptionSaveCommandInterceptor();

            // Can also set
            // AddCommandInterceptor
            // DeleteCommandInterceptor
            // ModifyCommandInterceptor 
        }
    }
}