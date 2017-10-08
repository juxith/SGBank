using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using SGBank.Data;
using SGBank.Models;
using Moq;
using SGBank.Models.Interfaces;

namespace SGBankTest
{

    [TestFixture]
    public class FileAccountTest
    {
        private const string _path = @"C:\Repos\dotnet-judy-thao\SGBankNew\FileDataSeedTest.txt";
        private const string _originalData = @"C:\Repos\dotnet-judy-thao\SGBankNew\FileData.txt";

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
            File.Copy(_originalData, _path);
        }

        [Test]
        public void CanReadDataFromFile()
        {
            FileAccountRepository test = new FileAccountRepository(_path);

            var checkData = test.LoadAccount("11111");

            Assert.AreEqual(checkData.AccountNumber, "11111");
            Assert.AreEqual(checkData.Name, "Free Customer");
            Assert.AreEqual(checkData.Balance, 100);
            Assert.AreEqual(checkData.Type, AccountType.Free);
        }
    }
}
