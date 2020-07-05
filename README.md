# CheckReceiptSDK
Библиотека для получения информации по чекам от ФНС

Пример вызова методов:

Регистрации:
```csharp
var result = await FNS.RegistrationAsync("some@mail.com", "Name", "+79991234567");
```
Восстановления пароля:
```csharp
var result = await FNS.RestorePasswordAsync("+79991234567");
```
Проверки существования чека:
```csharp
var date = DateTime.Parse("2018-05-18T22:05:00");
var result = await FNS.CheckAsync("8710000101337659", "94248", "815426975", date, 235.61);
```
Получения детальной информации по чеку:
```csharp
var result = await FNS.ReceiveAsync("8710000101337659", "94248", "815426975", "+79991234567", "123456");
```

### Для проверки работоспособности библиотеки можно вручную прогнать тесты, при этом не забывая подставлять корректные значения в TestCase параметрах
