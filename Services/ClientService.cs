using bank_api.Dtos.Client;
using bank_api.Models;

public class ClienteService
{
    private IClienteRepository _clienteRepository;

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

    public string AddCliente(CreateClientRequest clienteDto)
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

    public Client? GetClient(long clientId)
    {
        return _clienteRepository.GetId(clientId);
    }

    public string? UpdateClient(UpdateClientRequest clienteDto)
    {
        var client = new Client
        {
            Id = clienteDto.Id,
            Name = clienteDto.Name,
            Document = clienteDto.Document,
            Phone = clienteDto.Phone
        };
        _clienteRepository.Update(client);

        return "Cliente atualizado com sucesso!";
    }

    public void DeleteClient(long id)
    {
        _clienteRepository.Delete(id);
    }
}
