namespace NRepository.Core
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Repository.Containers;
    using EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Utility.Logging;

    internal static class IRepositoryExtensions
    {
        private static readonly ILogger Logger = ContosoUniversity.Core.Logging.LogManager.CreateLogger(typeof(IRepositoryExtensions));

        public static ValidationMessageCollection Save(
            this IRepository repository,
            EntityStateWrapperContainer entityContainer,
            Func<DbUpdateConcurrencyException, ValidationMessageCollection> dbUpdateConcurrencyExceptionFunc = null,
            Func<RetryLimitExceededException, ValidationMessageCollection> retryLimitExceededExceptionFunc = null)
        {
            try
            {
                entityContainer.UnitsOfWork().ToList().ForEach(uow =>
                {
                    UpdateStates(repository, uow);
                    repository.Save();
                });

                return new ValidationMessageCollection();
            }
            catch (DbUpdateConcurrencyException dbUpdateEx)
            {
                Logger.Warn(dbUpdateEx, dbUpdateEx.Message);
                if (dbUpdateConcurrencyExceptionFunc != null)
                    return dbUpdateConcurrencyExceptionFunc(dbUpdateEx);

                var validationDetails = new ValidationMessageCollection();
                validationDetails.Add(new ValidationMessage(string.Empty, dbUpdateEx.ToString()));
                return validationDetails;

            }
            catch (RetryLimitExceededException rleEx)
            {
                Logger.Error(rleEx, rleEx.Message);

                if (retryLimitExceededExceptionFunc != null)
                    return retryLimitExceededExceptionFunc(rleEx);

                var validationDetails = new ValidationMessageCollection();
                validationDetails.Add(new ValidationMessage(string.Empty, "Unable to save changes. Try again, and if the problem persists, see your system administrator."));
                return validationDetails;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
        }

        private static void UpdateStates<T>(IRepository repository, IEnumerable<EntityStateWrapper<T>> entities) where T : class
        {
            if (entities.Any())
            {
                entities.Where(p => p.State == State.Added).ToList().ForEach(p => repository.Add(p.Entity));
                entities.Where(p => p.State == State.Deleted).ToList().ForEach(p => repository.Delete(p.Entity));
                entities.Where(p => p.State == State.Modified).ToList().ForEach(p => repository.Modify(p.Entity));
                entities.Where(p => p.State == State.Unchanged).ToList().ForEach(p => repository.UpdateEntityState(p.Entity, EntityState.Unchanged));
            }
        }
    }
}
