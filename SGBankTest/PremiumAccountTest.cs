using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.BLL.WithdrawlRules;
using SGBank.Models.Responses;
using SGBank.BLL.DepositRules;

namespace SGBankTest
{
    [TestFixture]
    public class PremiumAccountTest
    {
        [TestCase("33333", "Premium Account", 1000, AccountType.Free, 100, 1000, false)]//Not Premium type
        [TestCase("33333", "Premium Account", 100, AccountType.Premium, -100, 100, false)]//negative number withdrawn
        [TestCase("33333", "Premium Account", 1000, AccountType.Premium, 100, 1100, true)]//Success
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance,
                                               AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRules();

            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;
            
            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(response.Success, expectedResult);
            Assert.AreEqual(account.Balance, newBalance);
        }

        [TestCase("33333", "Premium Account", 1000, AccountType.Free, -100, 1000, false)]//Not Premium type
        [TestCase("33333", "Premium Account", 100, AccountType.Premium, 100, 100, false)]//Positive number withdrawn
        [TestCase("33333", "Premium Account", 100, AccountType.Premium, -601, 100, false)]// withdrew over overdraft limit
        [TestCase("33333", "Premium Account", 1000, AccountType.Premium, -100, 900, true)]//Success
        public void PremiumAccountWithdrawRulesTest(string accountNumber, string name, decimal balance, 
                                                    AccountType type, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdrawl withdraw = new PremiumAccountWithdrawRules();

            Account account = new Account();
            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = type;
            
            AccountWithdrawlResponse response = withdraw.Withdrawl(account, amount);

            Assert.AreEqual(response.Success, expectedResult);
            Assert.AreEqual(account.Balance, newBalance);
        }


    }
}
