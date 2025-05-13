using Moq;
using src.Application.Services;
using src.Domain.Entities;
using src.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Application
{
    public class AccountServiceTest
    {
        [Fact(DisplayName = nameof(Reset))]
        public void Reset()
        {
            var repoMock = new Mock<IAccountRepository>();
            var service = new AccountService(repoMock.Object);
            service.Reset();
            repoMock.Verify(r => r.Reset(), Times.Once);
        }

        [Fact(DisplayName = nameof(Deposit))]
        public void Deposit()
        {
            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(r => r.GetById("123")).Returns((Account)null);

            Account createdAccount = null;
            repoMock.Setup(r => r.Create(It.IsAny<Account>()))
                    .Callback<Account>(a => createdAccount = a);

            var service = new AccountService(repoMock.Object);
            var result = service.Deposit("123", 50);

            Assert.NotNull(result);
            Assert.Equal("123", result.Id);
            Assert.Equal(50, result.Balance);
            Assert.NotNull(createdAccount);
            Assert.Equal(50, createdAccount.Balance);
        }

        [Fact(DisplayName = nameof(Withdraw))]
        public void Withdraw()
        {
            var account = new Account("123");
            account.Deposit(100);

            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(r => r.GetById("123")).Returns(account);

            var service = new AccountService(repoMock.Object);
            var result = service.Withdraw("123", 40);

            Assert.NotNull(result);
            Assert.Equal(60, result.Balance);
            repoMock.Verify(r => r.Update(It.Is<Account>(a => a.Balance == 60)), Times.Once);
        }

        [Fact(DisplayName = nameof(Transfer))]
        public void Transfer()
        {
            var origin = new Account("123");
            origin.Deposit(100);
            Account destination = null;

            var repoMock = new Mock<IAccountRepository>();
            repoMock.Setup(r => r.GetById("123")).Returns(origin);
            repoMock.Setup(r => r.GetById("321")).Returns((Account)null);

            repoMock.Setup(r => r.Create(It.IsAny<Account>()))
                .Callback<Account>(a => destination = a);

            var service = new AccountService(repoMock.Object);
            var result = service.Transfer("123", "321", 40);

            Assert.NotNull(result);
            Assert.Equal(60, result?.Origin.Balance);
            Assert.Equal(40, result?.Destination.Balance);
            Assert.NotNull(destination);
            Assert.Equal("321", destination.Id);

            repoMock.Verify(r => r.Update(origin), Times.Once);
            repoMock.Verify(r => r.Update(destination), Times.Once);
        }
    }
}
