using bank_api.Dtos.Transactions;
using bank_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace bank_api.Controllers
{
    [ApiController]
    [Route("api")]
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
            var resultado = _transactionService.AddDeposit(idClient, idAccount, transactionDto);

            return CreatedAtAction(nameof(GetTransactions), resultado);
        }

        [HttpGet("clients/{idClient}/accounts/{idAccount}/transactions/withdraws")]
        public ActionResult<IEnumerable<TransactionDTO>> GetTransactionsDraws([FromRoute] int idClient, [FromRoute] int idAccount)
        {
            return Ok(_transactionService.GetWithDraws(idClient, idAccount));
        }

        [HttpPost("clients/{idClient}/accounts/{idAccount}/transactions/withdraws")]
        public ActionResult<string> CreateTransactionDraws([FromRoute] int idClient, [FromRoute] int idAccount, [FromBody] CreateTransactionRequest transactionDto)
        {
            var resultado = _transactionService.AddWithDraws(idClient, idAccount, transactionDto);

            return CreatedAtAction(nameof(GetTransactions), resultado);
        }


    }
}
