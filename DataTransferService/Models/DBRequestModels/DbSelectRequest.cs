using DataTransferService.Models.DBRequestModels;

namespace DataTransferService.Models.DatabaseModels
{
    /// <summary>
    /// Represents a request model for selecting data from a database.
    /// Includes database connection details and a select query.
    /// </summary>
    public class DbSelectRequest : IDbRequest
    {
        public string? DbType { get; set; }
        public string? ConnectionString { get; set; }

        /// <summary>
        /// The SELECT query to retrieve data from the database.
        /// </summary>
        public string? SelectQuery { get; set; }
    }
}
