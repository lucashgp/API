namespace src.Domain.Entities
{
    public class Account
    {
        public string Id { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string id)
        {
            Id = id;
            Balance = 0;
        }
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        public Boolean Withdraw(decimal amount)
        {
            if (Balance < amount) return false;
            Balance -= amount;
            return true;
        }
    }
}
