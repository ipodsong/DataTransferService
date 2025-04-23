using DataTransferService.Loggers;
using DataTransferService.Models.RequestModels;
using System.Data;

namespace DataTransferService.Base
{
    /// <summary>
    /// Abstract base class that defines the template for a data transfer process.
    /// Subclasses must implement the data retrieval and transfer logic.
    /// </summary>
    public abstract class TransferProcessBase
    {
        /// <summary>
        /// Executes the full transfer process: retrieves data from the source 
        /// and transfers it to the destination.
        /// </summary>
        /// <param name="transferRequest">The request containing transfer configuration and parameters.</param>
        public async Task TransferProcessAsync(ITransferRequest transferRequest, ILogger logger)
        {
            try
            {
                DataTable sourceDt = await GetDataFromSourceAsync(transferRequest);
                if (sourceDt != null) {
                    await TransferDataAsync(transferRequest, sourceDt);
                }
                else
                {
                    throw new Exception("An error occurred while retrieving source data.");
                }
            }
            catch (Exception ex) {
                string className = GetType().Name;
                logger.LogError(LogEvent.TransferError, ex, "The {className} operation was terminated due to an unexpected error.", className);
            }
        }

        /// <summary>
        /// Retrieves data from the source based on the transfer request.
        /// </summary>
        /// <param name="transferRequest">The request containing source configuration.</param>
        /// <returns>A DataTable containing the retrieved source data.</returns>
        protected abstract Task<DataTable> GetDataFromSourceAsync(ITransferRequest transferRequest);

        /// <summary>
        /// Transfers the retrieved data to the target destination.
        /// </summary>
        /// <param name="transferRequest">The request containing target configuration.</param>
        /// <param name="sourceDt">The data to be transferred.</param>
        protected abstract Task TransferDataAsync(ITransferRequest transferRequest, DataTable sourceDt);
    }
}
