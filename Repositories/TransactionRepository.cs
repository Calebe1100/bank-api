using bank_api.Dtos.Transactions;
using bank_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace bank_api.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetDeposits( int idAccount);
        void AddDeposit(Transaction transactionDto);
    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddDeposit(Transaction transactionDto)
        {
            _context.Transactions.Add(transactionDto);
            _context.SaveChanges();
        }

        public IEnumerable<Transaction> GetDeposits( int idAccount)
        {
            return _context.Transactions.Where(c => c.IdAccount == idAccount).ToList().OrderByDescending(c => c.CreationDate);
        }
    }
}
