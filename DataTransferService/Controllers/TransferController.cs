using DataTransferService.Models.RequestModels;
using DataTransferService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DataTransferService.Controllers
{
    /// <summary>
    /// Handles data transfer requests from clients.
    /// </summary>
    [Route("api/transfer")]
    [ApiController]
    public class TransferController(TransferService transferService) : ControllerBase
    {
        /// <summary>
        /// Registers a new data transfer request.
        /// </summary>
        /// <param name="db2DbRequests"></param>
        /// <returns>An HTTP 200 OK response if successful.</returns>
        [HttpPost]
        [Route("regist")]
        public async Task<IActionResult> RegistAsync([FromBody] List<Db2DbRequest> db2DbRequests)
        {
            await transferService.RegistDb2DbTransferTaskAsync(db2DbRequests);

            return Ok();
        }
    }
}
