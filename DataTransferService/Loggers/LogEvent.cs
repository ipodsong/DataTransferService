namespace DataTransferService.Loggers
{
    /// <summary>
    /// A global static class for categorizing logging events across the application.
    /// </summary>
    public static class LogEvent
    {
        /// <summary>
        /// Event ID for failures during transfer task execution.
        /// </summary>
        public static readonly EventId TransferError = new(1001, "TransferError");

        /// <summary>
        /// Event ID for failures when calling external APIs.
        /// </summary>
        public static readonly EventId APICallError = new(1021, "APICallError");

        /// <summary>
        /// Event ID for failures during database update operations.
        /// </summary>
        public static readonly EventId DbUpdateError = new(1031, "DbUpdateError");
    }
}
