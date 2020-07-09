namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Represents response received on receipt's existence verification
    /// </summary>
    public sealed class CheckResult : Result
    {
        /// <summary>
        /// Receipt exists in Russian Federal Tax Service
        /// </summary>
        public bool ReceiptExists { get; internal set; }

        internal CheckResult()
        { }
    }
}
