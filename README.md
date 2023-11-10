# WorkingWithExcelFile.Console
## Описание проекта:
WorkWithExcelFile - это консольное приложение, разработанное для работы с данными, хранящимися в файлах формата Microsoft Excel (.xlsx). Проект предоставляет возможность ввода пути к файлу с данными, чтения информации о продуктах, клиентах и заказах, а также выполнения нескольких запросов к этим данным.

## Архитектура проекта:
# WorkWithExcelFile (Solution)

## Core (Project)
- Data (Folder)
  - [Client.cs](./Core/Data/Client.cs)
  - [Order.cs](./Core/Data/Order.cs)
  - [Product.cs](./Core/Data/Product.cs)
- [OrderInfo.cs](./Core/OrderInfo.cs)

## BusinessLogic (Project)
- Excel (Folder)
  - [ExcelPath.cs](./BusinessLogic/Excel/ExcelPath.cs)
  - [ClientReader.cs](./BusinessLogic/Excel/ClientReader.cs)
  - [OrderReader.cs](./BusinessLogic/Excel/OrderReader.cs)
  - [ProductReader.cs](./BusinessLogic/Excel/ProductReader.cs)
- [OrderInfoCombine.cs](./BusinessLogic/OrderInfoCombine.cs)

## UI (Project)
- [Program.cs](./UI/Program.cs)

## Описание структуры:
### Core (Project):
Хранит базовые классы данных.
Data содержит классы Client, Order, Product.
OrderInfo объединяет необходимые поля из таблиц Клиенты и Товары.

### BusinessLogic (Project):
Отвечает за бизнес-логику приложения.
Excel содержит классы для работы с Excel: ExcelPath, ClientReader, OrderReader, ProductReader.
OrderInfoCombine - класс, объединяющий методы для работы с данными.

### UI (Project):
Содержит пользовательский интерфейс и взаимодействие с пользователем.
Program.cs - точка входа, отображение меню и вызов методов для работы с данными.

## Принципы архитектуры:

### Разделение ответственности (SRP): 
аждый проект отвечает за свою область - Core за хранение данных, BusinessLogic за бизнес-логику, UI за взаимодействие с пользователем.
### Модульность: 
Классы разделены по проектам в соответствии с их функциональностью.
### ООП принципы: 
Использование классов для представления данных и их обработки.
### Удобство сопровождения: 
Четкое разделение бизнес-логики и пользовательского интерфейса делает код более поддерживаемым и расширяемым.
Такая структура позволяет легко расширять функциональность, изменять интерфейс и вносить правки в бизнес-логику, не затрагивая другие части приложения.
