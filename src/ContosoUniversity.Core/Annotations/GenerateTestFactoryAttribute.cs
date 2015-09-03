namespace ContosoUniversity.Core.Annotations
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GenerateTestFactoryAttribute : Attribute
    {
    }
}
