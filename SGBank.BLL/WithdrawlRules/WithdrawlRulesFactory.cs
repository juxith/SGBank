using SGBank.BLL.WithdrawlRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.DepositRules
{
    public class WithdrawlRulesFactory
    {
        public static IWithdrawl Create(AccountType type)
        {
            switch (type)
            {
                case AccountType.Free:
                    return new FreeAccountWithdrawlRules();
                case AccountType.Basic:
                    return new BasicAccountWithdrawRules();
                case AccountType.Premium:
                    return new PremiumAccountWithdrawRules();
            }
            throw new Exception("Account type is not supported!");
        }
    }
}
