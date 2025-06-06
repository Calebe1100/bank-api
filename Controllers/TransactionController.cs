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
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("clients/{idClient}/accounts/{idAccount}/transactions")]
        public ActionResult<IEnumerable<TransactionDTO>> GetTransactions([FromRoute] int idClient, [FromRoute] int idAccount)
        {
            return Ok(_transactionService.GetTransactions(idClient, idAccount));
        }

        [HttpPost("clients/{idClient}/accounts/{idAccount}/transactions/deposits")]
        public ActionResult<string> CreateTransaction([FromRoute] int idClient, [FromRoute] int idAccount, [FromBody] CreateTransactionRequest transactionDto)
        {
            transactionDto.Type = (int)TransactionEnum.Deposit;
            var resultado = _transactionService.AddDeposit(idClient, idAccount, transactionDto);

            return Ok(resultado);
        }


        [HttpPost("clients/{idClient}/accounts/{idAccount}/transactions/withdraws")]
        public ActionResult<string> CreateTransactionDraws([FromRoute] int idClient, [FromRoute] int idAccount, [FromBody] CreateTransactionRequest transactionDto)
        {
            transactionDto.Type = (int)TransactionEnum.WithDraw;
            var resultado = _transactionService.AddWithDraws(idClient, idAccount, transactionDto);

            return Ok(resultado);
        }


    }
}
