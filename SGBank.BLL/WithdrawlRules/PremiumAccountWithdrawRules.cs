using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Responses;

namespace SGBank.BLL.WithdrawlRules
{
    public class PremiumAccountWithdrawRules : IWithdrawl
    {
        public AccountWithdrawlResponse Withdrawl(Account account, decimal amount)
        {
            AccountWithdrawlResponse response = new AccountWithdrawlResponse();

            if(account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: non-Premium account hit a Premium Account Rule. Contact IT";
                return response;
            }

            if(amount >= 0)
            {
                response.Success = false;
                response.Message = "Entry must be a negative number";
                return response;
            }

            if(amount + account.Balance < -500)
            {
                response.Success = false;
                response.Message = "This is more than $500 overdraft limit.";
                return response;
            }

            response.Success = true;
            response.OldBalance = account.Balance;
            response.Account = account;
            response.Amount = amount;
            account.Balance += amount;

            return response;
        }
    }
}
