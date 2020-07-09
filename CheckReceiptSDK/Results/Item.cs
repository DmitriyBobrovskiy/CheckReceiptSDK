using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Represents position in a receipt. In different receipts parameters filled differently.
    /// </summary>
    [DataContract]
    public class Item
    {
        /// <summary>
        /// Amount in kopecks
        /// </summary>
        [DataMember]
        public int Sum { get; internal set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [DataMember]
        public double Quantity { get; internal set; }
        /// <summary>
        /// Price in kopecks
        /// </summary>
        [DataMember]
        public int Price { get; internal set; }
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public int Nds { get; internal set; }
        /// <summary>
        /// VAT amount paid by 10% rate. In kopecks.
        /// Not sure if still is used.
        /// </summary>
        [DataMember(Name = "nds10", IsRequired = false)]
        public int Nds10Sum { get; internal set; }
        /// <summary>
        /// VAT amount paid by 18% rate. In kopecks.
        /// Not sure if still is used.
        /// </summary>
        [DataMember(Name = "nds18", IsRequired = false)]
        public int Nds18Sum { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Properties { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Modifiers { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        public int PaymentType { get; internal set; }
    }
}
