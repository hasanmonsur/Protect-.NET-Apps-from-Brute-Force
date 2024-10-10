namespace WebSeriLogApi.Helpers
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class LogExcludeAttribute : Attribute
    {
        // This attribute does not need any properties
    }
}
