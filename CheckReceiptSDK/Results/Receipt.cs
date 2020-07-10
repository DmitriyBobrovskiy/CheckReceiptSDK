using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Represents receipt. In different receipts parameters can be filled differently.
    /// </summary>
    [DataContract]
    public class Receipt
    {
        #region Money
        /// <summary>
        /// Total amount in kopecks
        /// </summary>
        [DataMember]
        public int TotalSum { get; internal set; }
        /// <summary>
        /// Amount paid by cash in kopecks
        /// </summary>
        [DataMember]
        public int CashTotalSum { get; internal set; }
        /// <summary>
        /// Amount paid by card in kopecks
        /// </summary>
        [DataMember]
        public int EcashTotalSum { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember]
        public int PrepaidSum { get; internal set; }
        #endregion

        #region Cashbox
        /// <summary>
        /// Fiscal document sign/attribute, so called ФП, ФПД
        /// </summary>
        [DataMember]
        public ulong FiscalSign { get; internal set; }
        /// <summary>
        /// Fiscal document number, so called ФД
        /// </summary>
        [DataMember]
        public int FiscalDocumentNumber { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember]
        public int ReceiptCode { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember]
        public int RequestNumber { get; internal set; }
        /// <summary>
        /// Fiscal number, so called ФН
        /// </summary>
        [DataMember(Name = "fiscalDriveNumber")]
        public string FiscalNumber { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public string RawData { get; internal set; }
        /// <summary>
        /// Number of the shift that sold the items
        /// </summary>
        [DataMember]
        public int ShiftNumber { get; internal set; }
        /// <summary>
        /// Cashbox registration number
        /// </summary>
        [DataMember]
        public string KktRegId { get; internal set; }
        /// <summary>
        /// No description.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string MachineNumber { get; internal set; }
        #endregion

        #region Store
        /// <summary>
        /// Seller personal tax number (ИНН)
        /// </summary>
        [DataMember(Name = "userInn")]
        public string RetailInn { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public string RetailPlaceAddress { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public string RetailPlace { get; internal set; }
        /// <summary>
        /// Store name
        /// </summary>
        [DataMember(Name = "user", IsRequired = false)]
        public string StoreName { get; internal set; }
        /// <summary>
        /// Cashier/operator name
        /// </summary>
        [DataMember(Name = "operator")]
        public string Cashier { get; internal set; }
        /// <summary>
        /// Seller email address
        /// </summary>
        [DataMember(Name = "sellerAddress", IsRequired = false)]
        public string SellerEmailAddress { get; internal set; }
        #endregion

        #region Operation
        /// <summary>
        /// Operation type. Looks like purchase/sell
        /// </summary>
        [DataMember]
        public int OperationType { get; internal set; }
        /// <summary>
        /// Date of the transaction
        /// </summary>
        [DataMember(Name = "dateTime")]
        public DateTime ReceiptDateTime { get; internal set; }
        /// <summary>
        /// Goods/services which have been sold.
        /// </summary>
        [DataMember]
        public List<Item> Items { get; internal set; }
        #endregion

        #region Tax
        /// <summary>
        /// Tax system type
        /// </summary>
        [DataMember]
        public int TaxationType { get; internal set; }
        /// <summary>
        /// No description. Some VAT related amount in kopecks.
        /// </summary>
        [DataMember(Name = "ndsNo")]
        public int Nds { get; internal set; }
        /// <summary>
        /// No description. Some VAT related amount in kopecks.
        /// </summary>
        [DataMember(Name = "ndsCalculated18")]
        public int NdsCalculated { get; internal set; }
        #endregion

        #region Other
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public int ProvisionSum { get; internal set; }
        /// <summary>
        /// Email of the buyer
        /// </summary>
        [DataMember(IsRequired = false)]
        public string BuyerAddress { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public int CreditSum { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public int InternetSign { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public string MessageFiscalSign { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public string PropertiesData { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public PropertiesUser PropertiesUser { get; internal set; }
        /// <summary>
        /// Federal tax service web address
        /// </summary>
        [DataMember(IsRequired = false)]
        public string FnsSite { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember(IsRequired = false)]
        public string AddressToCheckFiscalSign { get; internal set; }
        #endregion

        #region Obsolete
        /// <summary>
        /// No description. Looks like it's obsolete.
        /// If it's not, please contact developer <see href="https://github.com/DmitriyBobrovskiy/CheckReceiptSDK"/>
        /// </summary>
        [DataMember(IsRequired = false)]
        [Obsolete]
        public List<object> StornoItems { get; internal set; }
        /// <summary>
        /// No description. Looks like it's obsolete.
        /// If it's not, please contact developer <see href="https://github.com/DmitriyBobrovskiy/CheckReceiptSDK"/>
        /// </summary>
        [DataMember(IsRequired = false)]
        [Obsolete]
        public List<object> Modifiers { get; internal set; }
        /// <summary>
        /// No description. Looks like it's obsolete.
        /// If it's not, please contact developer <see href="https://github.com/DmitriyBobrovskiy/CheckReceiptSDK"/>
        /// </summary>
        [DataMember(IsRequired = false)]
        [Obsolete]
        public List<object> Message { get; internal set; }
        /// <summary>
        /// No description. Looks like it's obsolete.
        /// If it's not, please contact developer <see href="https://github.com/DmitriyBobrovskiy/CheckReceiptSDK"/>
        /// </summary>
        [DataMember(IsRequired = false)]
        [Obsolete]
        public List<object> Properties { get; internal set; }
        /// <summary>
        /// VAT paid by 18% rate in kopecks. Looks like it's obsolete. 
        /// If it's not, please contact developer <see href="https://github.com/DmitriyBobrovskiy/CheckReceiptSDK"/>
        /// </summary>
        [DataMember(Name = "nds18")]
        [Obsolete]
        public int TotalNds18Sum { get; internal set; }
        /// <summary>
        /// VAT paid by 18% rate in kopecks. Looks like it's obsolete. 
        /// If it's not, please contact developer <see href="https://github.com/DmitriyBobrovskiy/CheckReceiptSDK"/>
        /// </summary>
        [DataMember(Name = "nds10")]
        [Obsolete]
        public int TotalNds10Sum { get; internal set; }
        #endregion
    }

    /// <summary>
    /// No description
    /// </summary>
    [DataContract]
    public class PropertiesUser
    {
        /// <summary>
        /// No description
        /// </summary>
        [DataMember]
        public string PropertyValue { get; internal set; }
        /// <summary>
        /// No description
        /// </summary>
        [DataMember]
        public string PropertyName { get; internal set; }
    }
}
