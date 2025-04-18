namespace DataTransferService.Models.RequestModels
{
    public interface ITransferRequest
    {
        /// <summary>
        /// Requester's ID.
        /// </summary>
        public string? RequesterId { get; set; }

        /// <summary>
        /// Transmission reference date.
        /// </summary>
        public DateOnly BaseDate { get; set; }

        /// <summary>
        /// Optional metadata or description for the request
        /// </summary>
        public string? Description { get; set; }
    }
}
