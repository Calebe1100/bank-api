using bank_api.Dtos.Accounts;
using bank_api.Models;

namespace bank_api.Services
{
    public class AccountService
    {
        private IAccountRepository _accountRepository;
        private ClienteService clienteService;
        private TransactionService transactionService;

        public AccountService(IAccountRepository accountRepository, ClienteService clienteService, TransactionService transactionService)
        {
            _accountRepository = accountRepository;
            this.clienteService = clienteService;
            this.transactionService = transactionService;
        }

        public async Task<IEnumerable<AccountDTO>> GetAccounts(int idClient)
        {
            var accounts = await _accountRepository.GetByClient(idClient);
            return accounts.Select(c => new AccountDTO
            {
                IdClient = c.IdClient,
                Number = c.Number,
                Id = c.Id,
                Value = GetAccountValue(idClient, c)
            });
        }

        private double GetAccountValue(int idClient, Account c)
        {
            return this.transactionService.GetTransactions(idClient, c.Id).Sum(t => t.Value);
        }

        public string AddAccount(int idClient, CreateAccountRequest accountDto)
        {
            // Recupera o cliente para obter as iniciais
            var client = this.clienteService.GetClient(idClient);
            if (client == null)
            {
                return "Cliente não encontrado.";
            }

            string accountNumber;
            do
            {
                accountNumber = GenerateAccountNumber(client.Name);
            }
            while (_accountRepository.ExistsByNumber(accountNumber)); // Verifica se o número já existe

            var account = new Account
            {
                IdClient = idClient,
                Number = accountNumber,
            };

            _accountRepository.Add(account);
            return "Conta cadastrada com sucesso!";
        }


        public Account? GetAccount(int idClient,long accountId)
        {
            return _accountRepository.GetId(idClient, accountId);
        }


        public void DeleteAccount(long idClient, long id)
        {
            _accountRepository.Delete(idClient, id);
        }


        internal Account GetAccountByNumber(int idClient, string number)
        {
            return _accountRepository.GetNumber(idClient, number);
        }

        private string GenerateAccountNumber(string clientName)
        {
            string initials = new string(clientName
                .ToUpper()
                .Where(char.IsLetter)
                .Take(2)
                .ToArray());

            // Garante que tenha 2 letras (completa com 'X' se necessário)
            if (initials.Length < 2)
            {
                initials = initials.PadRight(2, 'X');
            }

            var random = new Random();
            string randomDigits = random.Next(0, 999999).ToString("D6");

            return $"{initials}{randomDigits}";
        }
        //public string? UpdateAccount(UpdateAccountRequest accountDto)
        //{
        //    var account = new Account
        //    {
        //        Name = accountDto.Number,
        //        Document = accountDto.Document,
        //        Phone = accountDto.Phone
        //    };
        //    _accountRepository.Update(account);

        //    return "Account atualizado com sucesso!";
        //}

    }
}
