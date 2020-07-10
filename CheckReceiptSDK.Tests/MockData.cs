namespace CheckReceiptSDK.Tests
{
    internal static class MockData
    {
        public const string LoginSuccessResponse = @"
        {
           ""email"": ""<email_used_for_registration>"",
           ""name"": ""<name_used_for_registration>""
        }";

        public const string ReceiveReceiptContent = @"
        {
           ""document"": {""receipt"": {
           ""operationType"": 1,
           ""fiscalSign"": 3522207165,
           ""dateTime"": ""2018-05-17T17:57:00"",
           ""rawData"": ""AwAzAREEEAA4NzEwMDAwMTAwNTE4MzEzDQQUADAwMDExOTM1MTQwNDE0MDUgICAg+gMMADc4MjU3MDYwODYgIBAEBAAJ2gAA9AMEAGzC/Vo1BAYAMQTSDyLSDgQEABYBAAASBAQAogAAAB4EAQAB/AMCADwPPAQPAD0EAwCKrqQ+BAQARzYzNyMERQAGBCcAKjM0OTIyNzcgTkVTVC6MruAuTUFYSUIukZKQgJeAkoWLLjE0MKyrNwQCAJ8P/wMEAAZAQg8TBAIAnw9PBAIAbAH9Aw4AhK6ro+PopaKgIICtraAHBAIAPA85BAEAAE8EAgBsARgEDACAo+Cu4q7goyCOjo7xAyoANjIwMDE3LCCjLiCFqqDipeCoraHj4KMsIOOrLiCAp6itoCwgpC4gMTimHwQBAAE="",
           ""totalSum"": 3900,
           ""nds10"": 364,
           ""userInn"": ""7825706086"",
           ""taxationType"": 1,
           ""operator"": ""<Данные кассира>"",
           ""fiscalDocumentNumber"": 54812,
           ""properties"": [   {
              ""value"": ""G637"",
              ""key"": ""Код""
           }],
           ""receiptCode"": 3,
           ""requestNumber"": 162,
           ""user"": ""Агроторг ООО"",
           ""kktRegId"": ""0001193514041405"",
           ""fiscalDriveNumber"": ""8710000100518392"",
           ""items"": [   {
              ""sum"": 3999,
              ""price"": 3999,
              ""name"": ""*3492277 NEST.Мор.MAXIB.СТРАЧАТЕЛ.140мл"",
              ""quantity"": 1,
              ""nds10"": 364
           }],
           ""ecashTotalSum"": 0,
           ""retailPlaceAddress"": ""620017, г. Екатеринбург, ул. Азина, д. 18ж"",
           ""cashTotalSum"": 3900,
           ""shiftNumber"": 278
        }}}";
    }
}
