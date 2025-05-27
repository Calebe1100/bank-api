using bank_api.Dtos.Client;
using bank_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClientController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClienteDTO>> GetClients()
    {
        return Ok(_clienteService.GetClientes());
    }

    [HttpPost]
    public ActionResult<string> CreateClient([FromBody] CreateClientRequest clienteDto)
    {
        var resultado = _clienteService.AddCliente(clienteDto);
        if (resultado.Contains("já cadastrado"))
        {
            return BadRequest(resultado);
        }
        return CreatedAtAction(nameof(GetClients), resultado);
    }

    [HttpGet("clients/{id}")]
    public ActionResult<string> GetClient([FromRoute] long id)
    {
        return Ok(_clienteService.GetClient(id));
    }

    [HttpPut("clients/{id}")]
    public ActionResult<string> UpdateClient([FromBody] UpdateClientRequest clienteDto, [FromRoute] long id)
    {
        clienteDto.Id = id;
        var resultado = _clienteService.UpdateClient(clienteDto);

        return CreatedAtAction(nameof(GetClients), resultado);
    }

    [HttpDelete("clients/{id}")]
    public ActionResult<string> DeleteClient([FromRoute] long id)
    {
        _clienteService.DeleteClient(id);
        return NoContent();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (model.Username == "admin" && model.Password == "123")
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, model.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("knab"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "bank-issuer",
                audience: "bank-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized();
    }

}
