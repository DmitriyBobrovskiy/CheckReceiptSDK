using CheckReceiptSDK.Results;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CheckReceiptSDK.Tests
{
    [Ignore("Integration tests should be run explicitly. " +
        "If you want to run them - remove this parameter, but don't forget to restore it before committing.")]
    public class IntegrationTests
    {
        private void WriteOutput(Result result)
        {
            TestContext.WriteLine($"{result.StatusCode} {result.Message}");
        }

        [Test]
        // TODO: fill your data here
        [TestCase("some@gmail.com", "user_name", "+79995554466")]
        public async Task Registration(string email, string name, string phone)
        {
            var result = await FNS.Instance.RegisterAsync(email, name, phone);
            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("+79995554466", "111555")]
        public async Task Login(string phone, string password)
        {
            var result = await FNS.Instance.LoginAsync(phone, password);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("+79995554466")]
        public async Task RestorePassword(string phone)
        {
            var result = await FNS.Instance.RestorePasswordAsync(phone);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("9280440300631425", "45549", "3488636555", "2020-02-11T17:57:00", 179)]
        public async Task Check(string fiscalNumber, string fiscalDocument, 
            string fiscalSign, DateTime date, decimal sum)
        {
            var result = await FNS.Instance.CheckAsync(fiscalNumber, fiscalDocument, fiscalSign, date, sum);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        // TODO: fill your data here
        [TestCase("9280440300631425", "45549", "3488636555", "+79995554466", "111555")]
        public async Task Receive(string fiscalNumber, string fiscalDocument, string fiscalSign, 
            string phone, string password)
        {
            var result = await FNS.Instance.ReceiveAsync(fiscalNumber, fiscalDocument, fiscalSign, phone, password);

            WriteOutput(result);
            Assert.IsTrue(result.IsSuccess);
        }
    }
}