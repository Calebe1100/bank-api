using bank_api.Models;

public interface IClienteRepository
{
    IEnumerable<Client> GetAll();
    Client GetByCpf(string cpf);
    void Add(Client cliente);
}

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Client> GetAll() => _context.Clients.ToList();

    public Client GetByCpf(string cpf) => _context.Clients.FirstOrDefault(c => c.Document == cpf);

    public void Add(Client cliente)
    {
        _context.Clients.Add(cliente);
        _context.SaveChanges();
    }
}
