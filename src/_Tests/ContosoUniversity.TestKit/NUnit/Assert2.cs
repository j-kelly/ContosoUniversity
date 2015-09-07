namespace NUnit.Framework
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System;
    using System.Linq;

    public static class Assert2
    {
        public static void CheckInvariantValidation(string errorMsg, Action action)
        {
            Assert.Throws<InvariantValidationException>(() =>
            {
                try { action(); }
                catch (InvariantValidationException) { throw; }
                catch { }
            }, string.Format("Missing invariant validation check: {0}", errorMsg));
        }

        public static void CheckContextualValidation(string errorMessage, Func<ValidationMessageCollection> func)
        {
            try
            {
                var validationaction = func();
                validationaction.AllValidationMessages.Count().ShouldEqual(1, string.Format("Missing validation check: {0}", errorMessage));
            }
            catch (InvariantValidationException) { throw; }
            catch (AssertionException aEx) { Assert.Fail(aEx.Message); }
            catch { Assert.Fail(string.Format("Missing validation: {0}", errorMessage)); }
        }

        public static void CheckContextualValidation(string validationErrorMessage, string propertyName, Func<ValidationMessageCollection> func)
        {
            try
            {
                var validationaction = func();

                var msg = validationaction.Errors.SingleOrDefault(p => p.ErrorMessage == validationErrorMessage && p.PropertyName == propertyName);
                msg.ShouldNotEqual(null, string.Format("Missing validation check: {0}", validationErrorMessage));
            }
            catch (InvariantValidationException) { throw; }
            catch (AssertionException aEx) { Assert.Fail(aEx.Message); }
            catch { Assert.Fail(string.Format("Missing validation: {0}", validationErrorMessage)); }
        }
    }
}
