using bank_api.Dtos.Accounts;
using bank_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bank_api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("clients/{idClient}/accounts")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts([FromRoute] int idClient)
        {
            return  Ok(await _accountService.GetAccounts(idClient));
        }

        [HttpPost("clients/{idClient}/accounts")]
        public ActionResult<string> CreateAccount([FromRoute] int idClient, [FromBody] CreateAccountRequest accountDto)
        {
            var resultado = _accountService.AddAccount(idClient, accountDto);
            if (resultado.Contains("já cadastrado"))
            {
                return BadRequest(resultado);
            }
            return CreatedAtAction(nameof(CreateAccount), resultado);
        }

        [HttpGet("clients/{idClient}/accounts/{id}")]
        public ActionResult<string> GetAccount([FromRoute] int idClient, [FromRoute] long id)
        {
            return Ok(_accountService.GetAccount(idClient, id));
        }

        //[HttpPut("clients/{idClient}/accounts/{id}")]
        //public ActionResult<string> UpdateAccount([FromBody] UpdateAccountRequest accountDto, [FromRoute] long id)
        //{
        //    accountDto.Id = id;
        //    var resultado = _accountService.UpdateAccount(accountDto);

        //    return CreatedAtAction(nameof(GetAccounts), resultado);
        //}

        [HttpDelete("clients/{idClient}/accounts/{id}")]
        public ActionResult<string> DeleteAccount([FromRoute] int idClient, [FromRoute] long id)
        {
            _accountService.DeleteAccount(idClient, id);
            return NoContent();
        }
    }
}
