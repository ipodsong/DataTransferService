using DataTransferService.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataTransferService.Loggers
{
    /// <summary>
    /// A custom logger implementation that logs messages to MongoDB.
    /// </summary>
    /// <typeparam name="T">The category type associated with the logger.</typeparam>
    public class MongoDBLogger<T> : ILogger where T : class
    {
        // The MongoDB collection where logs will be stored
        private readonly IMongoCollection<BsonDocument> _logCollection;

        /// <summary>
        /// Constructor that sets up the MongoDB connection and initializes the log collection.
        /// </summary>
        /// <param name="configuration">The configuration object used to retrieve MongoDB settings.</param>
        public MongoDBLogger(IConfiguration configuration)
        {
            // Retrieve MongoDB settings from appsettings.json
            var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDB:Database").Value;
            var collectionName = configuration.GetSection("MongoDB:Collection").Value;
            var userName = configuration.GetSection("MongoDB:Username").Value;
            var password = configuration.GetSection("MongoDB:Password").Value;

            // Validate required settings
            if (ValidationUtil.IsNullOrEmptyStrings(connectionString, databaseName, collectionName, userName, password))
            {
                throw new ArgumentException("Invalid MongoDB settings. Please check appsettings.json.");
            }

            // Set up MongoDB connection
            MongoUrl mongoUrl = new(connectionString);
            MongoClientSettings mongoSetting = MongoClientSettings.FromUrl(mongoUrl);

            // Set a connection timeout of 1 seconds
            mongoSetting.ConnectTimeout = TimeSpan.FromSeconds(5);
            mongoSetting.ServerSelectionTimeout = TimeSpan.FromSeconds(5);

            // Add credentials
            MongoCredential credential = MongoCredential.CreateCredential(databaseName, userName, password);
            mongoSetting.Credential = credential;

            // Initialize MongoDB client and database
            MongoClient connection = new(mongoSetting);
            IMongoDatabase database = connection.GetDatabase(databaseName);

            // Test connection (Ping command)
            try
            {
                var pingCommand = new BsonDocument("ping", 1);
                database.RunCommand<BsonDocument>(pingCommand);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to connect to MongoDB. Please verify connection settings.", ex);
            }

            // Initialize log collection
            _logCollection = database.GetCollection<BsonDocument>(collectionName);
        }

        /// <summary>
        /// Begins a logging scope. Not implemented in this logger.
        /// </summary>
        /// <typeparam name="TState">The type of the state object.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>Always returns null as scope logging is not supported.</returns>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        /// <summary>
        /// Determines whether the specified log level is enabled.
        /// Currently always returns true.
        /// </summary>
        /// <param name="logLevel">The log level to check.</param>
        /// <returns>True if the log level is enabled; otherwise, false.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Logs a message to MongoDB.
        /// </summary>
        /// <typeparam name="TState">The type of the state object.</typeparam>
        /// <param name="logLevel">The severity level of the log.</param>
        /// <param name="eventId">An identifier for the log event.</param>
        /// <param name="state">The state associated with the log.</param>
        /// <param name="exception">An optional exception to include in the log.</param>
        /// <param name="formatter">A function that formats the log message.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            // Create a BSON document for the log entry
            var logEntry = new BsonDocument
            {
                { "Timestamp", DateTime.UtcNow },
                { "LogLevel", logLevel.ToString() },
                { "EventId", eventId.Id },
                { "Message", formatter(state, exception) ?? "" },
                { "Exception", exception?.ToString() ?? "" }
            };

            // Insert log into MongoDB
            try
            {
                _logCollection.InsertOne(logEntry);
            }
            catch
            {
                Console.WriteLine("Failed to write log to MongoDB. Please check MongoDB settings.");
            }
        }
    }
}
