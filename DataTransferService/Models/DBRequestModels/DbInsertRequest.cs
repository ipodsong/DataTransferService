namespace DataTransferService.Models.DBRequestModels
{
    /// <summary>
    /// Represents a request model for inserting data into a database.
    /// Includes database connection details, target table name, and optional delete query.
    /// </summary>
    public class DbInsertRequest : IDbRequest
    {
        public string? DbType { get; set; }
        public string? ConnectionString { get; set; }

        /// <summary>
        /// The name of the target table where the data will be inserted.
        /// </summary>
        public string? TableName { get; set; }

        /// <summary>
        /// The delete query to be executed before the insert operation, if needed.
        /// </summary>
        public string? DeleteQuery { get; set; }
    }
}
