using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private static string _path;

        private List<Account> ListOfAccounts = new List<Account>();

        public FileAccountRepository(string path)
        {

            _path = path;
            CreateListOfAccounts();
        }

        // Specific exception, catches only file not found

        public void CreateListOfAccounts()
        {
            try
            {
                using (StreamReader accountReader = new StreamReader(_path))
                {
                    accountReader.ReadLine();

                    for (string line = accountReader.ReadLine(); line != null; line = accountReader.ReadLine())
                    {
                        string[] cells = line.Replace("\"", "").Split(',');
                        Account bankAccounts = new Account();
                        bankAccounts.AccountNumber = cells[0];
                        bankAccounts.Name = cells[1];
                        bankAccounts.Balance = decimal.Parse(cells[2]);
                        bankAccounts.Type = ReadAccountType(cells[3]);

                        ListOfAccounts.Add(bankAccounts);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file: {_path} was not found.");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private AccountType ReadAccountType(string accountType)
        {
            AccountType type = new AccountType();

            switch (accountType)
            {
                case "F":
                    type = AccountType.Free;
                    break;
                case "B":
                    type = AccountType.Basic;
                    break;
                case "P":
                    type = AccountType.Premium;
                    break;
                default:
                    throw new Exception("Account type not accepted");
            }
            return type;
        }

        public Account LoadAccount(string AccountNumber)
        {
            var loadAccount = ListOfAccounts.SingleOrDefault(acc => acc.AccountNumber == AccountNumber);

            return loadAccount;
        }

        public void SaveAccount(Account account)
        {
            CreateAccountFile(ListOfAccounts);
        }

        private string CreateCsvForAccount(Account account)
        {
            return string.Format("{0},{1},{2},{3}", account.AccountNumber, account.Name, account.Balance, ConvertTypeToString(account.Type));
        }

        public string ConvertTypeToString(AccountType accountType)
        {
            string toString = accountType.ToString();

            switch (accountType)
            {
                case AccountType.Free:
                    toString = "F";
                    break;
                case AccountType.Basic:
                    toString = "B";
                    break;
                default:
                    toString = "P";
                    break;
            }
            return toString;
        }

        private void CreateAccountFile(List<Account> account)
        {
            if (File.Exists(_path))
                File.Delete(_path);

            using (StreamWriter sw = new StreamWriter(_path))
            {
                sw.WriteLine("AccountNumber,Name,Balance,Type");

                foreach (var item in ListOfAccounts)
                {
                    sw.WriteLine(CreateCsvForAccount(item));
                }
            }
        }
    }
}
