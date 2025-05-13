using src.Domain.Entities;
using src.Domain.Interfaces;

namespace src.Infrastructure.Data
{
    public class AccountRepository : IAccountRepository
    {
        private static readonly List<Account> _Db = new();

        public Account GetById(string id)
        {
            Account account = _Db.Find(a => a.Id == id);
            return account;
        }
        public void Create(Account account)
        {
            _Db.Add(account);
        }
        public decimal Balance(Account account)
        {
            return account.Balance;
        }
        public void Update(Account account)
        {
            var existing = GetById(account.Id);
            existing = account;

        }
        public void Reset()
        {
            _Db.Clear();
        }
    }
}
