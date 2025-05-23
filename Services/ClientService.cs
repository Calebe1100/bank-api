using bank_api.Dtos;
using bank_api.Models;
using System.Collections.Generic;

public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public IEnumerable<ClienteDTO> GetClientes()
    {
        var clientes = _clienteRepository.GetAll();
        return clientes.Select(c => new ClienteDTO
        {
            Id = c.Id,
            Name = c.Name,
            Document = c.Document,
            Phone = c.Phone
        });
    }

    public string AddCliente(ClienteDTO clienteDto)
    {
        if (_clienteRepository.GetByCpf(clienteDto.Document) != null)
        {
            return "CPF já cadastrado.";
        }

        var client = new Client
        {
            Name = clienteDto.Name,
            Document = clienteDto.Document,
            Phone = clienteDto.Phone
        };

        _clienteRepository.Add(client);
        return "Cliente cadastrado com sucesso!";
    }
}
