
using bank_api.Dtos.Accounts;
using bank_api.Dtos.Transactions;
using bank_api.Models;
using bank_api.Repositories;
using System.Net.Sockets;

namespace bank_api.Services
{
    public class TransactionService
    {
        ITransactionRepository transactionRepository;
        ClienteService clienteService;
        AccountService accountService;

        public TransactionService(ITransactionRepository transactionRepository, ClienteService clienteService, AccountService accountService)
        {
            this.transactionRepository = transactionRepository;
            this.clienteService = clienteService;
            this.accountService = accountService;
        }

        internal string AddDeposit(int idClient, int idAccount, CreateTransactionRequest transactionDto)
        {
            if (this.clienteService.GetClient(idClient) == null)
            {
                return "Cliente não cadastrado.";
            }

            var account = this.accountService.GetAccountByNumber(idClient, idAccount.ToString());

            if (account == null)
            {
                return "Conta não cadastrado.";
            }

            var deposit = new Transaction
            {
                IdAccount = account.Id,
                Value = transactionDto.Value,
                Type = (short)Enums.TransactionEnum.Deposit
            };

            transactionRepository.AddDeposit(deposit);
            return "Deposito cadastrado com sucesso!";
        }

        internal object? AddWithDraws(int idClient, int idAccount, CreateTransactionRequest transactionDto)
        {
            if (this.clienteService.GetClient(idClient) == null)
            {
                return "Cliente não cadastrado.";
            }

            var account = this.accountService.GetAccountByNumber(idClient, idAccount.ToString());

            if (account == null)
            {
                return "Conta não cadastrado.";
            }

            var deposit = new Transaction
            {
                IdAccount = account.Id,
                Value = transactionDto.Value,
                Type = (short)Enums.TransactionEnum.WithDraw
            };

            transactionRepository.AddDeposit(deposit);
            return "Deposito cadastrado com sucesso!";

        }

        internal IEnumerable<TransactionDTO> GetTransactions(int idClient, int idAccount)
        {

            if (this.clienteService.GetClient(idClient) == null)
            {
                return [];
            }

            var transactions = transactionRepository.GetDeposits(idAccount);
            return transactions.ToList().Select(c => new TransactionDTO
            {
                Id = c.Id,
                IdAccount = c.IdAccount,
                Value = c.Value
            });
        }

        internal object? GetWithDraws(int idClient, int idAccount)
        {
            throw new NotImplementedException();
        }
    }
}
