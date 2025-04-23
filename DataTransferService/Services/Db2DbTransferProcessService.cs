using DataTransferService.Base;
using DataTransferService.Models.RequestModels;
using System.Data;

namespace DataTransferService.Services
{
    public class Db2DbTransferProcessService : TransferProcessBase
    {
        protected override Task<DataTable> GetDataFromSourceAsync(ITransferRequest transferRequest)
        {
            throw new NotImplementedException();
        }

        protected override Task TransferDataAsync(ITransferRequest transferRequest, DataTable sourceDt)
        {
            throw new NotImplementedException();
        }
    }
}
