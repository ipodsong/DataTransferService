using DataTransferService.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace DataTransferService.Models.RequestModels
{
    /// <summary>
    /// Represents a request to transfer data from one database to another.
    /// Includes information about the requester, base date, and both source and target DB operations.
    /// </summary>
    public class Db2DbRequest : ITransferRequest
    {
        [Required(ErrorMessage = "RequesterId is required.")]
        public string? RequesterId { get; set; }

        [Required(ErrorMessage = "BaseDate is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "BaseDate must be in yyyy-MM-dd format.")]
        public DateOnly BaseDate { get; set; }

        /// <summary>
        /// The database selection request containing query and connection info for the source DB.
        /// </summary>
        [Required(ErrorMessage = "dbSelectRequest is required.")]
        public DbSelectRequest? dbSelectRequest { get; set; }

        /// <summary>
        /// The database insert request containing table and query info for the destination DB.
        /// </summary>
        [Required(ErrorMessage = "dbInsertRequest is required.")]
        public DbInsertRequest? dbInsertRequest { get; set; }
    }

    /// <summary>
    /// Represents a request model for selecting data from a database.
    /// Includes database connection details and a select query.
    /// </summary>
    public class DbSelectRequest
    {
        [Required(ErrorMessage = "DbType is required.")]
        [AllowedDbTypes]
        public string? DbType { get; set; }

        [Required(ErrorMessage = "ConnectionString is required.")]
        public string? ConnectionString { get; set; }

        /// <summary>
        /// The SELECT query to retrieve data from the database.
        /// </summary>
        [Required(ErrorMessage = "SelectQuery is required.")]
        public string? SelectQuery { get; set; }
    }

    /// <summary>
    /// Represents a request model for inserting data into a database.
    /// Includes database connection details, target table name, and optional delete query.
    /// </summary>
    public class DbInsertRequest
    {
        [Required(ErrorMessage = "DbType is required.")]
        [AllowedDbTypes]
        public string? DbType { get; set; }

        [Required(ErrorMessage = "ConnectionString is required.")]
        public string? ConnectionString { get; set; }

        /// <summary>
        /// The name of the target table where the data will be inserted.
        /// </summary>
        [Required(ErrorMessage = "TableName is required.")]
        public string? TableName { get; set; }

        /// <summary>
        /// The delete query to be executed before the insert operation, if needed.
        /// </summary>
        public string? DeleteQuery { get; set; }
    }
}
