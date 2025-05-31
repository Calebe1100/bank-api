using bank_api.Dtos.Accounts;
using bank_api.Models;

namespace bank_api.Services
{
    public class AccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IEnumerable<AccountDTO> GetAccounts(int idClient)
        {
            var accounts = _accountRepository.GetByClient(idClient);
            return accounts.Select(c => new AccountDTO
            {
                IdClient = c.IdClient,
                Number = c.Number,
                Id = c.Id,
            });
        }

        public string AddAccount(int idClient, CreateAccountRequest accountDto)
        {
            if (_accountRepository.GetByNumber(accountDto.Number) != null)
            {
                return "CPF já cadastrado.";
            }

            var account = new Account
            {
                IdClient = accountDto.IdClient,
                Number = accountDto.Number,
            };

            _accountRepository.Add(account);
            return "Account cadastrado com sucesso!";
        }

        public Account? GetAccount(int idClient,long accountId)
        {
            return _accountRepository.GetId(idClient, accountId);
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

        public void DeleteAccount(long idClient, long id)
        {
            _accountRepository.Delete(idClient, id);
        }


        internal Account GetAccountByNumber(int idClient, string number)
        {
            return _accountRepository.GetNumber(idClient, number);
        }
    }
}
