using SGBank.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL
{
    public static class AccountManagerFactory
    {
        public static AccountManager Create()
        {
            //App.Settings is a dictionary.
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "FreeTest":
                    return new AccountManager(new FreeAccountTestRepository());
                case "BasicTest":
                    return new AccountManager(new BasicAccountTestRepository());
                case "PremiumTest":
                    return new AccountManager(new PremiumAccountTestRepository());
                case "FileAccounts":
                    return new AccountManager(new FileAccountRepository(@"C:\Repos\dotnet-judy-thao\SGBankNew\FileData.txt"));
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
