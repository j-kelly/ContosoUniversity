namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using NRepository.Core.Command;
    using System;

    public class ThrowOriginalExceptionSaveCommandInterceptor : ISaveCommandInterceptor
    {
        public int Save(ICommandRepository repository, Func<int> saveFunc) => saveFunc();

        public bool ThrowOriginalException => true;
    }
}
