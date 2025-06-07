using bank_api.Dtos.Tranfer;
using bank_api.Dtos.Transactions;
using bank_api.Enums;
using bank_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bank_api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly TransferService _transferService;

        public TransferController(TransferService transferService)
        {
            _transferService = transferService;
        }

        //[HttpGet("clients/{idClient}/accounts/{idAccount}/transfers")]
        //public ActionResult<IEnumerable<TransferDTO>> GetTransfers([FromRoute] int idClient, [FromRoute] int idAccount)
        //{
        //    return Ok(_transferService.GetTransfers(idClient, idAccount));
        //}

        [HttpPost("clients/{idClient}/accounts/{idAccount}/transfers")]
        public ActionResult<string> CreateTransfer([FromRoute] int idClient, [FromRoute] int idAccount, [FromBody] CreateTransferRequest transferDto)
        {
            var resultado = _transferService.AddTransfer(idClient, idAccount, transferDto);

            return Ok(resultado);
        }


    }
}
