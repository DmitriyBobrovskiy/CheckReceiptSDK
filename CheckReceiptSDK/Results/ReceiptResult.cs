using System.Runtime.Serialization;

namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Represents information received from Federal Tax Service when requested detailed information.
    /// </summary>
    public sealed class ReceiptResult : Result
    {
        /// <summary>
        /// Document information received from FTS
        /// </summary>
        public Document Document { get; internal set; }
        /// <summary>
        /// Raw response
        /// </summary>
        public string Content { get; internal set; }

        internal ReceiptResult()
        { }
    }

    [DataContract]
    internal class RootObject
    {
        [DataMember]
        public Document Document { get; internal set; }
    }
    /// <summary>
    /// Document information received from FTS
    /// </summary>
    [DataContract]
    public class Document
    {
        /// <summary>
        /// Receipt's information
        /// </summary>
        [DataMember]
        public Receipt Receipt { get; internal set; }
    }
}
