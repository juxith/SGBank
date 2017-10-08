using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.BLL.DepositRules;
using SGBank.Models.Responses;
using SGBank.BLL.WithdrawlRules;

namespace SGBankTest
{
    [TestFixture]
    public class BasicAccountTest
    {
        [TestCase("33333", "Basic Account", 100, AccountType.Free, 250, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 250, 350, true)]
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, 
                                                AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRules();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(response.Success, expectedResult );
            Assert.AreEqual(account.Balance, newBalance);
        }

        [TestCase("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)]
        [TestCase("33333", "Basic Account", 150, AccountType.Basic, -50, 100, true)]
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)]
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance,
                                                AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdrawl withdraw = new BasicAccountWithdrawRules();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawlResponse response = withdraw.Withdrawl(account, amount);

            Assert.AreEqual(response.Success, expectedResult);
            Assert.AreEqual(account.Balance, newBalance);
        }




    }
}
