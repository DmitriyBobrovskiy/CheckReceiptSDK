using System;

namespace CheckReceiptSDK.Resources
{
    internal static class Urls
    {
        private const string DateFormat = "yyyy-MM-ddThh:mm:ss";

        internal const string Registration = "https://proverkacheka.nalog.ru:9999/v1/mobile/users/signup";
        internal const string Login = "https://proverkacheka.nalog.ru:9999/v1/mobile/users/login";
        internal const string Restore = "https://proverkacheka.nalog.ru:9999/v1/mobile/users/restore";

        internal static string GetCheckUrl(string fiscalNumber, string fiscalDocument, string fiscalSign, DateTime date, decimal sum)
        {
            if (string.IsNullOrWhiteSpace(fiscalSign))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(fiscalSign));
            }
            if (string.IsNullOrWhiteSpace(fiscalNumber))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(fiscalNumber));
            }
            if (string.IsNullOrWhiteSpace(fiscalDocument))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(fiscalDocument));
            }
            if (date.Equals(DateTime.MinValue))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(date));
            }

            return $"https://proverkacheka.nalog.ru:9999/v1/ofds/*/inns/*/fss/{fiscalNumber}/operations/1/tickets/{fiscalDocument}" +
                $"?fiscalSign={fiscalSign}&date={date.ToString(DateFormat)}&sum={GetSum(sum)}";
        }

        private static string GetSum(decimal sum)
            => $"{Convert.ToInt32(Math.Round(sum, 2, MidpointRounding.AwayFromZero) * 100)}";

        internal static string GetReceiveUrl(string fiscalNumber, string fiscalDocument, string fiscalSign)
        {
            if (string.IsNullOrWhiteSpace(fiscalSign))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(fiscalSign));
            }
            if (string.IsNullOrWhiteSpace(fiscalNumber))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(fiscalNumber));
            }
            if (string.IsNullOrWhiteSpace(fiscalDocument))
            {
                throw new ArgumentException("Недопустимое значение параметра", nameof(fiscalDocument));
            }
            return $"https://proverkacheka.nalog.ru:9999/v1/inns/*/kkts/*/fss/{fiscalNumber}/tickets/{fiscalDocument}?fiscalSign={fiscalSign}&sendToEmail=no";
        }
    }
}