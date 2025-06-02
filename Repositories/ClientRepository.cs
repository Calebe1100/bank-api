using bank_api.Dtos.Client;
using bank_api.Models;

public interface IClienteRepository
{
    IEnumerable<Client> GetAll();
    Client GetByCpf(string cpf);
    Client GetId(long id);
    void Add(Client cliente);
    void Update(Client clienteDto);
    void Delete(long id);
    Client? GetByDocument(string document);
}

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Client>  GetAll() => _context.Clients.ToList();

    public Client GetByCpf(string cpf) => _context.Clients.FirstOrDefault(c => c.Document == cpf);

    public void Add(Client cliente)
    {
        _context.Clients.Add(cliente);
        _context.SaveChanges();
    }

    public Client GetId(long id)
    {
        return _context.Clients.FirstOrDefault(c => c.Id ==id);
    }

    public Client? GetByDocument(string document)
    {
        return _context.Clients.FirstOrDefault(c => c.Document == document);
    }

    public void Update(Client cliente)
    {
        _context.Clients.Update(cliente);
        _context.SaveChanges();
    }

    public void Delete(long id)
    {
        var client = _context.Clients.FirstOrDefault(x => x.Id == id);
        _context.Clients.Remove(client);
    }
}
