# CheckReceiptSDK
Library for getting receipts' information from Russian Federal Tax Service.  
Библиотека для получения информации по чекам от ФНС.

### Usage example:

Registration:
```csharp
var result = await FNS.Instance.RegistrationAsync("some@mail.com", "Name", "+79991234567");
```
Password restore:
```csharp
var result = await FNS.Instance.RestorePasswordAsync("+79991234567");
```
Receipt existence verification:
```csharp
var date = DateTime.Parse("2018-05-18T22:05:00");
var result = await FNS.Instance.CheckAsync("8710000101337659", "94248", "815426975", date, 235.61);
```
Getting receipt's detailed information:
```csharp
var result = await FNS.Instance.ReceiveAsync("8710000101337659", "94248", "815426975", "+79991234567", "123456");
```

### Testing
There are two types of tests: unit tests and integration tests. Unit tests can be run as is. 
For running integration tests, `Ignore` attribute should be removed in `IntegrationTests` 
class and correct values should be provided for tests.