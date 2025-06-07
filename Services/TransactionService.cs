
using bank_api.Dtos.Accounts;
using bank_api.Dtos.Transactions;
using bank_api.Enums;
using bank_api.Models;
using bank_api.Repositories;
using System.Net.Sockets;

namespace bank_api.Services
{
    public class TransactionService
    {
        ITransactionRepository transactionRepository;
        ClienteService clienteService;
        IAccountRepository accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, ClienteService clienteService, IAccountRepository accountRepository)
        {
            this.transactionRepository = transactionRepository;
            this.clienteService = clienteService;
            this.accountRepository = accountRepository;
        }

        internal string AddDeposit(int idClient, int idAccount, CreateTransactionRequest transactionDto)
        {
            if (this.clienteService.GetClient(idClient) == null)
            {
                return "Cliente não cadastrado.";
            }

            var account = this.accountRepository.GetId(idClient, idAccount);

            if (account == null)
            {
                return "Conta não cadastrado.";
            }



            var deposit = new Transaction
            {
                IdAccount = account.Id,
                Value = transactionDto.Value,
                Type = (short)Enums.TypeEnum.Credit,
                Operation = (short)OperationEnum.Deposit,
                CreationDate = DateTime.Now,
            };

            transactionRepository.AddDeposit(deposit);

            if (transactionDto.Value > GetAccountValue(idClient, account))
            {
                var tax = new Transaction
                {
                    IdAccount = idAccount,
                    Value = transactionDto.Value * 0.1,
                    Type = (short)Enums.TypeEnum.Credit,
                    Operation = (short)OperationEnum.Bonus,
                    CreationDate = DateTime.Now,
                };

                this.transactionRepository.AddDeposit(tax);
            }

            return "Deposito cadastrado com sucesso!";
        }

        private double GetAccountValue(int idClient, Account c)
        {
            return this.GetTransactions(idClient, c.Id).Sum(t => t.Type == (int)TypeEnum.Credit ? t.Value : t.Value * -1);
        }

        internal object? AddWithDraws(int idClient, int idAccount, CreateTransactionRequest transactionDto)
        {
            if (this.clienteService.GetClient(idClient) == null)
            {
                return "Cliente não cadastrado.";
            }

            var account = this.accountRepository.GetId(idClient, idAccount);

            if (account == null)
            {
                return "Conta não cadastrado.";
            }

            var accountSald = GetTransactions(idClient, idAccount).Sum(x => x.Value) + account.CreditLimit;
            if (double.Abs(transactionDto.Value) > accountSald)
            {
                return "Saldo insulficiente.";
            }

            var deposit = new Transaction
            {
                IdAccount = account.Id,
                Value = transactionDto.Value,
                Type = (short)Enums.TypeEnum.Debit,
                Operation = (short)OperationEnum.WithDraw,
                CreationDate = DateTime.Now,
            };

            transactionRepository.AddDeposit(deposit);
            return "Saque realizado com sucesso!";

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
                Value = c.Value,
                Type = c.Type,
                Operation = c.Operation,
                CreationDate = c.CreationDate.ToShortDateString(),

            });
        }

        internal object? GetWithDraws(int idClient, int idAccount)
        {
            throw new NotImplementedException();
        }
    }
}
