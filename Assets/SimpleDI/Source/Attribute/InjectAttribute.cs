using System;

namespace SB
{
    /// <summary>
    /// The attribute to indicate the method for dependency injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class InjectAttribute : Attribute
    {
    }
}