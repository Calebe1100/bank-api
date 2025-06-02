using bank_api.Dtos.Client;
using bank_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api")]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClientController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet("clients")]
    public ActionResult<IEnumerable<ClienteDTO>> GetClients()
    {
        return Ok(_clienteService.GetClientes());
    }

    [HttpPost("clients")]
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
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        // Buscar usuário no banco de dados
        var usuario = _clienteService.GetByDocument(model.Document);

        if (usuario == null)
        {
            return Unauthorized(); // Usuário não encontrado
        }

        // Validar senha
        bool senhaValida = BCrypt.Net.BCrypt.Verify(model.Password, usuario.PasswordHash);

        if (!senhaValida)
        {
            return Unauthorized(); // Senha inválida
        }

        // Criar claims para JWT
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, model.Document),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uma_chave_muito_mais_segura_e_longa_123!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "bank-issuer",
            audience: "bank-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token),  name = usuario.Name, idClient = usuario.Id });
    }


}
