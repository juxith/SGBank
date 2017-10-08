using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBankTest
{
    [TestFixture]
    public class FreeAccountTest
    {
        [Test]
        public void CanLoadFreeAccoungTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookUpResponse response = manager.LookUpAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [TestCase("12345", "Free Account", 100, AccountType.Free, 250, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, 150, true)]
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, 
                                                AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IDeposit test = new FreeAccountDepositRules();

            Account newAcct = new Account();
            newAcct.AccountNumber = accountNumber;
            newAcct.Name = name;
            newAcct.Balance = balance;
            newAcct.Type = accountType;

            AccountDepositResponse response = test.Deposit(newAcct, amount);
     
            Assert.AreEqual(response.Success, expectedResult);
            Assert.AreEqual(newAcct.Balance, newBalance);
        }

        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, 100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -150,  100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Basic, -50, 100,false)]
        [TestCase("12345", "Free Account", 50, AccountType.Free, -70, 50, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -50, 50, true)]
        public void FreeAccountWithdrawlRuleTest(string accountNumber, string name, decimal balance,
                                                AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdrawl test = new FreeAccountWithdrawlRules();

            Account newAcct = new Account();
            newAcct.AccountNumber = accountNumber;
            newAcct.Name = name;
            newAcct.Balance = balance;
            newAcct.Type = accountType;

            AccountWithdrawlResponse response = test.Withdrawl(newAcct, amount);

            Assert.AreEqual(response.Success, expectedResult);
            Assert.AreEqual(newAcct.Balance, newBalance);
        }
    }
}
