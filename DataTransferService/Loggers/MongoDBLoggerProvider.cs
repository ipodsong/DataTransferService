namespace DataTransferService.Loggers
{
    /// <summary>
    /// Provides logging functionality using MongoDB.
    /// </summary>
    public class MongoDBLoggerProvider(IConfiguration configuration) : ILoggerProvider
    {
        /// <summary>
        /// Creates a new instance of MongoDBLogger based on the specified category name.
        /// </summary>
        /// <param name="categoryName">The name of the logging category.</param>
        /// <returns>A MongoDBLogger instance.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new MongoDBLogger<object>(configuration);
        }

        /// <summary>
        /// Releases any resources used by the logger provider.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
