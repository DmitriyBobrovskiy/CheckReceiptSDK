namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Класс, представляющий ответ, полученный в результате проверки существования чека
    /// </summary>
    public sealed class CheckResult : Result
    {
        /// <summary>
        /// Существует ли чек в базе ФНС?
        /// </summary>
        public bool ReceiptExists { get; internal set; }

        internal CheckResult()
        { }
    }
}
