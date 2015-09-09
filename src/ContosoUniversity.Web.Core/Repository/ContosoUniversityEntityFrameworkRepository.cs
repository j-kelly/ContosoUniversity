namespace ContosoUniversity.Web.Core.Repository
{
    using ContosoUniversity.Domain.Core.Repository;
    using Interceptors;
    using NRepository.EntityFramework;

    public class ContosoUniversityEntityFrameworkRepository : EntityFrameworkRepository
    {
        public ContosoUniversityEntityFrameworkRepository()
            : base(new ContosoDbContext(), new ContosoUniversityRepositoryInterceptors())
        {
        }
    }
}
