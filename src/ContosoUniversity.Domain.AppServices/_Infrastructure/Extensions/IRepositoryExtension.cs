namespace NRepository.Core
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using System;
    using System.Data.Entity.Infrastructure;
    using Utility.Logging;

    internal static class IRepositoryExtensions
    {
        private static readonly ILogger Logger = ContosoUniversity.Core.Logging.LogManager.CreateLogger(typeof(IRepositoryExtensions));

        public static ValidationMessageCollection SaveWithValidation(
            this IRepository repository,
            Func<DbUpdateConcurrencyException, ValidationMessageCollection> dbUpdateConcurrencyExceptionFunc = null,
            Func<RetryLimitExceededException, ValidationMessageCollection> retryLimitExceededExceptionFunc = null)
        {
            try
            {
                repository.Save();
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
    }
}
