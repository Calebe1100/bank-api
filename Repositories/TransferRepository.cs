using bank_api.Models;

namespace bank_api.Repositories
{
    public interface ITransferRepository
    {
        void AddTransfer(Transaction transferDto);
    }
    public class TransferRepository : ITransferRepository
    {
        private readonly AppDbContext _context;

        public TransferRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddTransfer(Transaction transferDto)
        {
            _context.Transactions.Add(transferDto);
            _context.SaveChanges();
        }


    }
}
