using CheckReceiptSDK.Results;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CheckReceiptSDK.Tests
{
    public class FNSTest
    {
        private void WriteOutput(Result result)
        {
            TestContext.WriteLine($"{result.StatusCode} {result.Message}");
        }

        [Test]
        // TODO: fill your data here
        [TestCase("some@gmail.com", "Ваше имя", "+79995554466")]
        public async Task Registration(string email, string name, string phone)
        {
            var result = await FNS.RegistrationAsync(email, name, phone);
            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("+79995554466", "111555")]
        public async Task Login(string phone, string password)
        {
            var result = await FNS.LoginAsync(phone, password);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("+79995554466")]
        public async Task RestorePassword(string phone)
        {
            var result = await FNS.RestorePasswordAsync(phone);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("9280440300631425", "45549", "3488636555", "2020-02-11T17:57:00", 179)]
        public async Task Check(string fiscalNumber, string fiscalDocument, 
            string fiscalSign, DateTime date, decimal sum)
        {
            var result = await FNS.CheckAsync(fiscalNumber, fiscalDocument, fiscalSign, date, sum);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("9280440300631425", "45549", "3488636555", "+79995554466", "111555")]
        public async Task Receive(string fiscalNumber, string fiscalDocument, string fiscalSign, 
            string phone, string password)
        {
            var result = await FNS.ReceiveAsync(fiscalNumber, fiscalDocument, fiscalSign, phone, password);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}