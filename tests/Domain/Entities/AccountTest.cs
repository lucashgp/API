using src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace tests.Domain.Entities
{
    public class AccountTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        public void Instantiate()
        {
            var account = new Account("123");
            Assert.NotNull(account);
            Assert.Equal("123", account.Id);
            Assert.Equal(0, account.Balance);
        }
        [Fact(DisplayName = nameof(InstantiateWithError))]
        public void InstantiateWithError()
        {
            var account = new Account("1234");
            Assert.NotEqual("123", account.Id);
            Assert.NotEqual(1, account.Balance);
        }
        [Fact(DisplayName = nameof(Deposit))]
        public void Deposit()
        {
            var account = new Account("123");
            var initialBalance = account.Balance;
            decimal depositAmount = 100;
            account.Deposit(depositAmount);
            Assert.Equal(initialBalance + depositAmount, account.Balance);
        }

        [Fact(DisplayName = nameof(Withdraw))]
        public void Withdraw()
        {
            var account = new Account("123");
            account.Deposit(200);
            decimal withdrawAmount = 150;
            var result = account.Withdraw(withdrawAmount);
            Assert.True(result);
            Assert.Equal(50, account.Balance);
        }

        [Fact(DisplayName = nameof(WithdrawWithError))]
        public void WithdrawWithError()
        {
            var account = new Account("123");
            account.Deposit(50);
            decimal withdrawAmount = 100;

            var result = account.Withdraw(withdrawAmount);
            Assert.False(result);
            Assert.Equal(50, account.Balance);
        }

    }
}
