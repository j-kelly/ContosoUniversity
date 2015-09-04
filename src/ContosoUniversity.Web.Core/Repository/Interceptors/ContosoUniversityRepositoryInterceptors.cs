namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using NRepository.Core;

    // Add intercepts in this class for Add, Delete, Modify, Save & query
    public class ContosoUniversityRepositoryInterceptors : RepositoryInterceptors
    {
        public ContosoUniversityRepositoryInterceptors()
        {
            SaveCommandInterceptor = new ContosoUniversitySaveCommandInterceptor();
            DeleteCommandInterceptor = new SoftDeleteDeleteCommandInterceptor();
            AddCommandInterceptor = new TrackedEntitiesAddCommandInterceptor();
            ModifyCommandInterceptor = new TrackedEntitiesModifyCommandInterceptor();
        }
    }
}