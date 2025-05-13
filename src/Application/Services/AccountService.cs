using src.Application.DTOs;
using src.Domain.Entities;
using src.Domain.Interfaces;

namespace src.Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public decimal Balance(string id)
        {
            Account account = _accountRepository.GetById(id);
            decimal balance = _accountRepository.Balance(account);
            return balance;

        }
        public AccountDTO GetAccount(string id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null) return null;

            return new AccountDTO { Id = account.Id, Balance = account.Balance };
        }

        public void Reset()
        {
            _accountRepository.Reset();
        }
        public AccountDTO Deposit(string accountId, decimal amount)
        {
            if (amount <= 0) throw new InvalidOperationException("Valor inválido.");

            var account = _accountRepository.GetById(accountId);
            if (account == null)
            {
                var newAccount = new Account(accountId);
                newAccount.Deposit(amount);
                _accountRepository.Create(newAccount);
                return new AccountDTO { Id = newAccount.Id, Balance = newAccount.Balance };
            }

            account.Deposit(amount);
            _accountRepository.Update(account);
            return new AccountDTO { Id = account.Id, Balance = account.Balance };
        }

        public AccountDTO Withdraw(string accountId, decimal amount)
        {
            var account = _accountRepository.GetById(accountId);
            if (account == null || account.Balance < amount) return null;

            account.Withdraw(amount);
            _accountRepository.Update(account);
            return new AccountDTO { Id = account.Id, Balance = account.Balance };
        }

        public (AccountDTO Origin, AccountDTO Destination)? Transfer(string originId, string destinationId, decimal amount)
        {
            var origin = _accountRepository.GetById(originId);
            if (origin == null || origin.Balance < amount) return null;

            var destination = _accountRepository.GetById(destinationId);
            if (destination == null)
            {
                destination = new Account(destinationId);
                _accountRepository.Create(destination);
            }

            origin.Withdraw(amount);
            destination.Deposit(amount);

            _accountRepository.Update(origin);
            _accountRepository.Update(destination);

            return (
                new AccountDTO { Id = origin.Id, Balance = origin.Balance },
                new AccountDTO { Id = destination.Id, Balance = destination.Balance }
            );
        }

    }
}
