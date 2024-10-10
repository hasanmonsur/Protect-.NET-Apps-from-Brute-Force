namespace WebSeriLogApi.Helpers
{
    public static class LoggerExtensions
    {
        public static void LogInformationWithoutSensitiveData<T>(this ILogger logger, string message, T obj)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(LogExcludeAttribute)));

            var logData = properties.ToDictionary(p => p.Name, p => p.GetValue(obj));

            logger.LogInformation(message, logData);
        }
    }
}
