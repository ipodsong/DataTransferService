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
    }
}
