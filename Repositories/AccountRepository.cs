using bank_api.Models;
using Microsoft.EntityFrameworkCore;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetByClient(int idClient);
    Account GetByNumber(string cpf);
    Account GetId(int idClient, long id);
    void Add(Account account);
    Task UpdateAsync(Account account);
    void Delete(long idClient, long id);
    bool ExistsByNumber(string accountNumber);
    Account GetNumber(int idClient, string number);
    Task<IEnumerable<Account>> GetFull();
}

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetByClient(int idClient)
    {
        return await _context.Accounts
                             .Where(c => c.IdClient == idClient)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetFull()
    {
        return await _context.Accounts
                             .ToListAsync();
    }

    public Account GetByNumber(string number) => _context.Accounts.FirstOrDefault(c => c.Number == number);

    public void Add(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public Account GetId(int idClient, long id)
    {
        return _context.Accounts.FirstOrDefault(c => c.IdClient == idClient && c.Id == id);
    }

    public async Task UpdateAsync(Account account)
    {
        var trackedEntity = await _context.Accounts
                                          .FirstOrDefaultAsync(a => a.Id == account.Id && a.IdClient == account.IdClient);

        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity).CurrentValues.SetValues(account);
            await _context.SaveChangesAsync();
        }
        // Se preferir, você pode lançar exceção aqui se não encontrar.
    }


    public void Delete(long idClient, long id)
    {
        var client = _context.Accounts.FirstOrDefault(x => x.IdClient == idClient && x.Id == id);
        _context.Accounts.Remove(client);
    }

    public Account GetNumber(int idClient, string number)
    {
        return _context.Accounts.FirstOrDefault(c => c.IdClient == idClient && c.Number == number);
    }

    public bool ExistsByNumber(string accountNumber)
    {
        return _context.Accounts.Any(a => a.Number == accountNumber);
    }

}
