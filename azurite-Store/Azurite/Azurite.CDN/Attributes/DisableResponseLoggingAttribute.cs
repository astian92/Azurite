using System;

namespace Azurite.CDN.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class DisableResponseLoggingAttribute : Attribute
    {
    }
}