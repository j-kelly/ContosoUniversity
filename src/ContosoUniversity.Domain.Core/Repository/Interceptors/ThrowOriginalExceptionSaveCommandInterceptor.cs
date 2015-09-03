namespace ContosoUniversity.DAL
{
    using System;
    using NRepository.Core.Command;

    public class ThrowOriginalExceptionSaveCommandInterceptor : ISaveCommandInterceptor
    {
        public int Save(ICommandRepository repository, Func<int> saveFunc)
        {
            return saveFunc();
        }

        public bool ThrowOriginalException
        {
            get { return true; }
        }
    }
}
