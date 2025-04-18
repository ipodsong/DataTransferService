namespace DataTransferService.Models.DBRequestModels
{
    /// <summary>
    /// Represents a basic interface for database requests.
    /// Includes the database type and connection string.
    /// </summary>
    public interface IDbRequest
    {
        /// <summary>
        /// The type of the database (e.g., MySQL, MSSQL, Oracle).
        /// </summary>
        public string? DbType { get; set; }

        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        public string? ConnectionString { get; set; }
    }
}
