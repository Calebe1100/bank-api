using bank_api.Dtos.Client;
using Microsoft.AspNetCore.Mvc;

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
}
