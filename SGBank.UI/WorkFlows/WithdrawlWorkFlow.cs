using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public class WithdrawlWorkFlow
    {
        public void Execute()
        {
            AccountManager accountManager = AccountManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Withdraw from Account");
            Console.WriteLine("----------------------------------");

            Console.Write("Enter an account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter a negative withdrawl amount: ");

            decimal.TryParse(Console.ReadLine(), out decimal amount);
          
            AccountWithdrawlResponse response = accountManager.Withdrawl(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Withdrawl completed");
                Console.WriteLine($"Account number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Withdrawn: {response.Amount:c}");
                Console.WriteLine($"New balance: {response.Account.Balance:C}");
            }
            else
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}