using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CheckReceiptSDK.Tests
{
    public class UnitTests
    {
        [Test]
        [TestCase("some@gmail.com", "User_name", "+79995554466")]
        public async Task Registration(string email, string name, string phone)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(System.Net.HttpStatusCode.NoContent, ""));
            var result = await instance.RegisterAsync(email, name, phone);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        [TestCase("+79995554466", "111555")]
        public async Task Login(string phone, string password)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                HttpStatusCode.OK, MockData.LoginSuccessResponse));
            var result = await instance.LoginAsync(phone, password);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        [TestCase("+79995554466")]
        public async Task RestorePassword(string phone)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                System.Net.HttpStatusCode.NoContent, ""));
            var result = await instance.RestorePasswordAsync(phone);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        [TestCase("9280440300631425", "45549", "3488636555", "2020-02-11T17:57:00", 179)]
        public async Task CheckReceiptIsFound(string fiscalNumber, string fiscalDocument,
            string fiscalSign, DateTime date, decimal sum)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                System.Net.HttpStatusCode.NoContent, ""));
            var result = await instance.CheckAsync(fiscalNumber, fiscalDocument, fiscalSign, date, sum);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        [TestCase("9280440300631425", "45549", "3488636555", "2020-02-11T17:57:00", 179)]
        public async Task CheckReceiptIsNotFound(string fiscalNumber, string fiscalDocument,
            string fiscalSign, DateTime date, decimal sum)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                HttpStatusCode.NotAcceptable, ""));
            var result = await instance.CheckAsync(fiscalNumber, fiscalDocument, fiscalSign, date, sum);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, result.StatusCode);
        }

        [Test]
        [TestCase("9280440300631425", "45549", "3488636555", "+79995554466", "111555")]
        public async Task Receive(string fiscalNumber, string fiscalDocument, string fiscalSign,
            string phone, string password)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                HttpStatusCode.OK, MockData.ReceiveReceiptContent));
            var result = await instance.ReceiveAsync(fiscalNumber, fiscalDocument, fiscalSign, phone, password);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        [TestCase("9280440300631425", "45549", "3488636555", "+79995554466", "111555")]
        public async Task ReceiveReceiptUserIsNotFound(string fiscalNumber, string fiscalDocument, string fiscalSign,
            string phone, string password)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                HttpStatusCode.Forbidden, "the user was not found or the specified password was not correct"));
            var result = await instance.ReceiveAsync(fiscalNumber, fiscalDocument, fiscalSign, phone, password);

            Assert.AreEqual(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Test]
        [TestCase("9280440300631425", "45549", "3488636555", "+79995554466", "111555")]
        public async Task ReceiveReceiptIsNotFound(string fiscalNumber, string fiscalDocument, string fiscalSign,
            string phone, string password)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                HttpStatusCode.NotAcceptable, ""));
            var result = await instance.ReceiveAsync(fiscalNumber, fiscalDocument, fiscalSign, phone, password);

            Assert.AreEqual(HttpStatusCode.NotAcceptable, result.StatusCode);
        }

        [Test]
        [TestCase("9280440300631425", "45549", "3488636555", "+79995554466", "111555")]
        public async Task ReceiveReceiptCheckDidNotCalled(string fiscalNumber, string fiscalDocument, string fiscalSign,
            string phone, string password)
        {
            var instance = new FNS(HttpMessageHandlerMock.GetHttpClient(
                HttpStatusCode.NoContent, ""));
            var result = await instance.ReceiveAsync(fiscalNumber, fiscalDocument, fiscalSign, phone, password);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}