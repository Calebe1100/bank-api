using bank_api.Dtos.Tranfer;
using bank_api.Repositories;
using bank_api.Models;
using bank_api.Enums;
using bank_api.Dtos.Transactions;

namespace bank_api.Services
{
    public class TransferService
    {
        ITransactionRepository transactionRepository;
        ClienteService clienteService;
        IAccountRepository accountRepository;

        public TransferService(ITransactionRepository transactionRepository, ClienteService clienteService, IAccountRepository accountRepository)
        {
            this.transactionRepository = transactionRepository;
            this.clienteService = clienteService;
            this.accountRepository = accountRepository;
        }

        internal string AddTransfer(int idClient, int idAccount, CreateTransferRequest transferDto)
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

            var accountSald = transactionRepository.GetDeposits(idAccount).Sum(x => x.Value) + account.CreditLimit;
            if (idClient != transferDto.TargetClient && (transferDto.Value + (transferDto.Value * 0.1) > accountSald))
            {
                return "Saldo insulficiente.";
            }

            if ( (transferDto.Value > accountSald))
            {
                return "Saldo insulficiente.";
            }

            var received = new Transaction
            {
                IdAccount = transferDto.TargetAccount,
                Value = transferDto.Value,
                Type = (short)Enums.TypeEnum.Credit,
                Operation = (short) OperationEnum.Transfer,
                CreationDate = DateTime.Now,
            };
            
            var sent = new Transaction
            {
                IdAccount = idAccount,
                Value = transferDto.Value,
                Type = (short)Enums.TypeEnum.Debit,
                Operation = (short) OperationEnum.Transfer,
                CreationDate = DateTime.Now,
            };

            this.transactionRepository.AddDeposit(received);
            this.transactionRepository.AddDeposit(sent);

            if (idClient != transferDto.TargetClient)
            {

                var tax = new Transaction
                {
                    IdAccount = idAccount,
                    Value = transferDto.Value * 0.1,
                    Type = (short)Enums.TypeEnum.Debit,
                    Operation = (short)OperationEnum.Tax,
                    CreationDate = DateTime.Now,
                };

                this.transactionRepository.AddDeposit(tax);

            }

            return "Transferencia realizada com sucesso!";
        }


    }
}
