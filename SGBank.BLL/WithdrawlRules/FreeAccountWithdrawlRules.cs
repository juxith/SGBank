using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.DepositRules
{
    public class FreeAccountWithdrawlRules : IWithdrawl
    {
        public AccountWithdrawlResponse Withdrawl(Account account, decimal amount)
        {
            AccountWithdrawlResponse response = new AccountWithdrawlResponse();
            if (account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: a non free account hit the Free Withdrawl Rule. Contact IT";
                return response;
            }

            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Entry must be a negative number";
                return response;
            }

            if (amount + account.Balance < 0)
            {
                response.Success = false;
                response.Message = $"Insufficient funds for withdrawl. Balance is {account.Balance}";
                return response;
            }

            if (amount < -100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot withdrawl more than $100 at a time.";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
