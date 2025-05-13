using src.Domain.Entities;

namespace src.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Account GetById(string id);
        void Create(Account account);
        void Update(Account account);
        decimal Balance(Account account);
        void Reset();
    }
}
