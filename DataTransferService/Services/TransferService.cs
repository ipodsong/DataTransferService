using DataTransferService.Loggers;
using DataTransferService.Models.RequestModels;
using System.Threading.Channels;

namespace DataTransferService.Services
{
    /// <summary>
    /// Handles queuing and executing asynchronous data transfer tasks with concurrency control.
    /// </summary>
    public class TransferService
    {
        #region Field

        // Unbounded channel to queue asynchronous tasks
        private readonly Channel<Func<Task>> channel = Channel.CreateUnbounded<Func<Task>>();

        // Semaphore to limit the number of concurrently running tasks
        private readonly SemaphoreSlim semaphore = new(15);

        // Logger for recording task execution details or errors
        private readonly ILogger<TransferService> logger;

        #endregion

        #region Constructor & Destructor

        /// <summary>
        /// Initializes the TransferService and starts listening to the task channel.
        /// </summary>
        /// <param name="logger">Logger</param>
        public TransferService(ILogger<TransferService> logger)
        {
            SetChannel(); // Begin listening for tasks
            this.logger = logger;
        }

        /// <summary>
        /// Destructor to complete the channel and stop task intake.
        /// </summary>
        ~TransferService()
        {
            // Gracefully complete the channel writer to signal shutdown
            channel.Writer.Complete();
        }

        #endregion

        #region Task Queue Handling

        /// <summary>
        /// Starts reading from the channel and triggers task execution as they arrive.
        /// This method runs in the background as long as the channel is open.
        /// </summary>
        private async void SetChannel()
        {
            // Continuously wait for new tasks as long as the channel is open
            while (await channel.Reader.WaitToReadAsync())
            {
                // Try to read all available tasks in the channel
                while (channel.Reader.TryRead(out Func<Task>? func))
                {
                    if (func != null)
                    {
                        // Execute the task without awaiting (fire and forget)
                        _ = ExecuteAction(func);
                    }
                }
            }
        }

        /// <summary>
        /// Executes a single asynchronous task while respecting the concurrency limit.
        /// </summary>
        /// <param name="func">The task to execute.</param>
        private async Task ExecuteAction(Func<Task> func)
        {
            // Wait for an available slot to execute the task
            await semaphore.WaitAsync();
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                logger.LogError(LogEvent.TransferError, ex, "Failed to execute transfer task.");
            }
            finally
            {
                // Release the semaphore slot for the next task
                semaphore.Release();
            }
        }

        /// <summary>
        /// Adds a new asynchronous task to the channel for queued execution.
        /// </summary>
        /// <param name="func">The task to be queued and executed.</param>
        public async Task AddTaskAsync(Func<Task> func)
        {
            await channel.Writer.WriteAsync(func);
        }

        #endregion

        #region Make/Regist Task
        public async Task RegistDb2DbTransferTaskAsync(List<Db2DbRequest> db2DbRequests)
        {
            var db2DbService = new Db2DbTransferProcessService();

            foreach(var db2DbRequest in db2DbRequests)
            {
                async Task transferTask() => await db2DbService.TransferProcessAsync(db2DbRequest, logger);

                await AddTaskAsync(transferTask);
            }
        }
        #endregion
    }
}
