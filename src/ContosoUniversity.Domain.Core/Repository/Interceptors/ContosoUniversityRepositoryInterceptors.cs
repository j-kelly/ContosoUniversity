namespace ContosoUniversity.DAL
{
    using Domain.Core.Repository.Projections.Factories;
    using NRepository.Core;

    // Add intercepts in this class for Add, Delete, Modify, Save & query
    public class ContosoUniversityRepositoryInterceptors : RepositoryInterceptors
    {
        public ContosoUniversityRepositoryInterceptors()
        {
            base.QueryInterceptor = new ContosoFactoryQueryInterceptor();
            base.SaveCommandInterceptor = new ThrowOriginalExceptionSaveCommandInterceptor();

            // Can also set
            // base.AddCommandInterceptor
            // base.DeleteCommandInterceptor
            // base.ModifyCommandInterceptor 
        }
    }
}