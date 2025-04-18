using DataTransferService.Models.DatabaseModels;
using DataTransferService.Models.DBRequestModels;

namespace DataTransferService.Models.RequestModels
{
    /// <summary>
    /// Represents a request to transfer data from one database to another.
    /// Includes information about the requester, base date, and both source and target DB operations.
    /// </summary>
    public class Db2DbRequest : ITransferRequest
    {
        public string? RequesterId { get; set; }
        public DateOnly BaseDate { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// The database selection request containing query and connection info for the source DB.
        /// </summary>
        public DbSelectRequest? dbSelectRequest { get; set; }

        /// <summary>
        /// The database insert request containing table and query info for the destination DB.
        /// </summary>
        public DbInsertRequest? dbInsertRequest { get; set; }
    }
}
