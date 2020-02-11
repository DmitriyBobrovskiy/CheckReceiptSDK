using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CheckReceiptSDK.Results
{
    /// <summary>
    /// Класс, описывающий информацию, получаемую в результате запроса от ФНС детальной информации по чеку
    /// </summary>
    public sealed class ReceiptResult : Result
    {
        /// <summary>
        /// Информация о документе, которая приходит из ФНС
        /// </summary>
        public Document Document { get; internal set; }

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
    /// Документ, который приходит из ФНС
    /// </summary>
    [DataContract]
    public class Document
    {
        /// <summary>
        /// Внутренняя информация о чеке
        /// </summary>
        [DataMember]
        public Receipt Receipt { get; internal set; }
    }
    /// <summary>
    /// Непосредственно сам чек. В разных чеках по разному заполнены параметры.
    /// </summary>
    [DataContract]
    public class Receipt
    {
        #region Money
        /// <summary>
        /// Общая сумма по чеку, в копейках
        /// </summary>
        [DataMember]
        public int TotalSum { get; internal set; }
        /// <summary>
        /// Сумма, оплаченная наличными, в копейках
        /// </summary>
        [DataMember]
        public int CashTotalSum { get; internal set; }
        /// <summary>
        /// Сумма, оплаченная безналичным способом оплаты, в копейках
        /// </summary>
        [DataMember]
        public int EcashTotalSum { get; internal set; }
        /// <summary>
        /// Сумма НДС оплаченная по ставке 18%, в копейках
        /// </summary>
        [DataMember(Name = "nds18")]
        public int TotalNds18Sum { get; internal set; }
        /// <summary>
        /// Сумма НДС оплаченная по ставке 10%, в копейках
        /// </summary>
        [DataMember(Name = "nds10")]
        public int TotalNds10Sum { get; internal set; }
        #endregion

        #region Cashbox
        /// <summary>
        /// Фискальный признак документа, также известный как ФП, ФПД
        /// </summary>
        [DataMember]
        public ulong FiscalSign { get; internal set; }
        /// <summary>
        /// Номер фискального документа
        /// </summary>
        [DataMember]
        public int FiscalDocumentNumber { get; internal set; }
        /// <summary>
        /// Код чека
        /// </summary>
        [DataMember]
        public int ReceiptCode { get; internal set; }
        /// <summary>
        /// Номер запроса
        /// </summary>
        [DataMember]
        public int RequestNumber { get; internal set; }
        /// <summary>
        /// Фискальный номер
        /// </summary>
        [DataMember(Name = "fiscalDriveNumber")]
        public string FiscalNumber { get; internal set; }
        /// <summary>
        /// Что-то вроде зашифрованной информации о чеке
        /// </summary>
        [DataMember(IsRequired = false)]
        public string RawData { get; internal set; }
        /// <summary>
        /// Номер смены
        /// </summary>
        [DataMember]
        public int ShiftNumber { get; internal set; }
        /// <summary>
        /// Регистрационный номер ККТ
        /// </summary>
        [DataMember]
        public string KktRegId { get; internal set; }
        #endregion

        #region Store
        /// <summary>
        /// ИНН продавца
        /// </summary>
        [DataMember(Name = "userInn")]
        public string RetailInn { get; internal set; }
        /// <summary>
        /// Адрес точки продажи
        /// </summary>
        [DataMember(IsRequired = false)]
        public string RetailPlaceAddress { get; internal set; }
        /// <summary>
        /// Наименование продавца
        /// </summary>
        [DataMember(Name = "user", IsRequired = false)]
        public string StoreName { get; internal set; }
        /// <summary>
        /// Данные кассира, который пробил чек
        /// </summary>
        [DataMember(Name = "operator")]
        public string Cashier { get; internal set; }
        /// <summary>
        /// Адрес электронной почты организации, отправившей информацию по чеку в ФНС
        /// </summary>
        [DataMember(Name = "senderAddress", IsRequired = false)]
        public string SenderEmailAddress { get; internal set; }
        #endregion

        #region Operation
        /// <summary>
        /// Тип операции. Полагаю продажа, покупка и т.д.
        /// </summary>
        [DataMember]
        public int OperationType { get; internal set; }
        /// <summary>
        /// Дата совершения операции
        /// </summary>
        [DataMember(Name = "dateTime")]
        public DateTime ReceiptDateTime { get; internal set; }
        /// <summary>
        /// Товары/услуги, участвующие в операции
        /// </summary>
        [DataMember]
        public List<Item> Items { get; internal set; }
        #endregion

        #region Other
        /// <summary>
        /// Тип системы налогообложения
        /// </summary>
        [DataMember]
        public int TaxationType { get; internal set; }
        /// <summary>
        /// Не понимаю что это
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> StornoItems { get; internal set; }
        /// <summary>
        /// Не понимаю что это
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Modifiers { get; internal set; }
        /// <summary>
        /// Не понимаю что это
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Message { get; internal set; }
        /// <summary>
        /// Не понимаю что это
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Properties { get; internal set; }
        #endregion
    }

    /// <summary>
    /// Класс, представляющий позицию в чеке. В разных чеках параметры заполнены по разному.
    /// </summary>
    [DataContract]
    public class Item
    {
        /// <summary>
        /// Сумма по позиции, в копейках
        /// </summary>
        [DataMember]
        public int Sum { get; internal set; }
        /// <summary>
        /// Количество
        /// </summary>
        [DataMember]
        public double Quantity { get; internal set; }
        /// <summary>
        /// Цена позиции, в копейках
        /// </summary>
        [DataMember]
        public int Price { get; internal set; }
        /// <summary>
        /// Наименование позиции
        /// </summary>
        [DataMember]
        public string Name { get; internal set; }
        /// <summary>
        /// Сумма НДС, оплаченная по ставке 10%, в копейках
        /// </summary>
        [DataMember(Name = "nds10", IsRequired = false)]
        public int Nds10Sum { get; internal set; }
        /// <summary>
        /// Сумма НДС, оплаченная по ставке 18%, в копейках
        /// </summary>
        [DataMember(Name = "nds18", IsRequired = false)]
        public int Nds18Sum { get; internal set; }
        /// <summary>
        /// Не понимаю что это
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Properties { get; internal set; }
        /// <summary>
        /// Не понимаю что это
        /// </summary>
        [DataMember(IsRequired = false)]
        public List<object> Modifiers { get; internal set; }
    }
}
