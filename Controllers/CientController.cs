using bank_api.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClienteController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClienteDTO>> GetClientes()
    {
        return Ok(_clienteService.GetClientes());
    }

    [HttpPost]
    public ActionResult<string> CreateCliente([FromBody] ClienteDTO clienteDto)
    {
        var resultado = _clienteService.AddCliente(clienteDto);
        if (resultado.Contains("já cadastrado"))
        {
            return BadRequest(resultado);
        }
        return CreatedAtAction(nameof(GetClientes), new { id = clienteDto.Id }, resultado);
    }
}
