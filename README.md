[![NuGet](https://img.shields.io/nuget/dt/CheckReceiptSDK.svg?label=NuGet)](https://www.nuget.org/packages/CheckReceiptSDK) 
![Nuget](https://img.shields.io/nuget/v/CheckReceiptSDK)
# CheckReceiptSDK
Library for getting receipts' information from Russian Federal Tax Service.  
Библиотека для получения информации по чекам от Федеральной Налоговой Службы (ФНС).

## Important: after upgrade it stopped working [see](https://github.com/DmitriyBobrovskiy/CheckReceiptSDK/issues/12). I didn't figure out yet if it is solvable.

### Usage example:

Registration:
```csharp
var result = await FNS.Instance.RegisterAsync("some@mail.com", "Name", "+79991234567");
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

### NB: not enough information to specify all the fields in receipt
