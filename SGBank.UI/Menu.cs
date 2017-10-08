using SGBank.UI.WorkFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("SG Bank Application");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. WithDraw");

                Console.WriteLine("\nQ to quit");
                Console.WriteLine("\nEnter selection: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AccountLookUpWorkFlow lookUpWorkFlow= new AccountLookUpWorkFlow();
                        lookUpWorkFlow.Execute();
                        break;
                    case "2":
                        DepositWorkFlow depositWorkFlow = new DepositWorkFlow();
                        depositWorkFlow.Execute();
                        break;
                    case "3":
                        //withdrawl
                        WithdrawlWorkFlow withDrawlWorkFlow = new WithdrawlWorkFlow();
                        withDrawlWorkFlow.Execute();
                        break;
                    case "Q":
                        return;

                }
            }
        }
    }


}
