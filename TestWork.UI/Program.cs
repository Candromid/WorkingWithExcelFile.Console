using System;
using System.Collections.Generic;
using TestWork.BusinessLogic;
using TestWork.BusinessLogic.Excel;
using TestWork.Core.Data;

namespace TestWork.UI
{
    public class Program
    {
        private static string filePath = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine("Выберите нужное действие:");
            Console.WriteLine("1. Запрос на ввод пути до файла с данными");
            Console.WriteLine("0. Выход");

            int choice;
            while (true)
            {
                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        filePath = GetUserInputFilePath();
                        ShowMainMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Выберите нужное действие:");
                Console.WriteLine("1. Запрос на ввод пути до файла с данными");
                Console.WriteLine("0. Выход");
            }

        }
            
        static void ShowMainMenu()
        {
            var (products, clients, orders) = LoadData();

            Console.WriteLine();
            Console.WriteLine("Выберите нужное действие:");
            Console.WriteLine("1. По наименованию товара выводить информацию о клиентах, заказавших этот товар");
            Console.WriteLine("2. Запрос на изменение контактного лица клиента");
            Console.WriteLine("3. Запрос на определение золотого клиента");
            Console.WriteLine("0. Выход в предыдущее меню");

            int choice;
            while (true)
            {
                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        GetOrdersByProductName(products, clients, orders);
                        break;
                    case 2:
                        UpdateContactPerson(clients);
                        break;
                    case 3:
                        GetGoldenClient(products, clients, orders);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Выберите нужное действие:");
                Console.WriteLine("1. По наименованию товара выводить информацию о клиентах, заказавших этот товар");
                Console.WriteLine("2. Запрос на изменение контактного лица клиента");
                Console.WriteLine("3. Запрос на определение золотого клиента");
                Console.WriteLine("0. Выход в предыдущее меню");
            }
        }

        static string GetUserInputFilePath()
        {
            string filePath;
            do
            {
                Console.WriteLine("Введите путь к файлу Excel с данными (путь не должен содержать кириллицу):");
                filePath = Console.ReadLine();

                if (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Неверный формат файла. Укажите путь к файлу с расширением .xlsx.");
                }
                else if (!System.IO.File.Exists(filePath))
                {
                    Console.WriteLine("Файл не найден. Проверьте правильность пути.");
                }
            } while (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(filePath));

            return filePath;
        }


        static (List<Product>, List<Client>, List<Order>) LoadData()
        {
            var productReader = new ProductReader();
            var clientReader = new ClientReader();
            var orderReader = new OrderReader();

            productReader.LoadExcelFile(filePath);
            clientReader.LoadExcelFile(filePath);
            orderReader.LoadExcelFile(filePath);

            var products = productReader.ReadProducts();
            var clients = clientReader.ReadClients();
            var orders = orderReader.ReadOrders();

            return (products, clients, orders);
        }

        static void GetOrdersByProductName(List<Product> products, List<Client> clients, List<Order> orders)
        {
            var orderInfoCombine = new OrderInfoCombine(products, clients, orders);

            Console.WriteLine("Введите наименование товара:");
            string productName = Console.ReadLine();

            var ordersInfo = orderInfoCombine.GetOrdersByProductName(productName);

            if (ordersInfo.Count == 0)
            {
                Console.WriteLine($"Товар с наименованием '{productName}' не найден в заявках.");
                return;
            }

            foreach (var orderInfo in ordersInfo)
            {
                Console.WriteLine($"Клиент: {orderInfo.ClientName}");
                Console.WriteLine($"Количество товара: {orderInfo.Quantity}");
                Console.WriteLine($"Цена за ед. товара: {orderInfo.UnitPrice}");
                Console.WriteLine($"Дата заказа: {orderInfo.OrderDate}");
                Console.WriteLine();
            }
        }

        static void UpdateContactPerson(List<Client> clients)
        {
            var clientReader = new ClientReader();
            clientReader.LoadExcelFile(filePath);

            Console.WriteLine("Введите название организации:");
            string organizationName = Console.ReadLine();

            Console.WriteLine("Введите новое ФИО контактного лица:");
            string newContactPersonFullName = Console.ReadLine();

            bool result = clientReader.UpdateContactPerson(organizationName, newContactPersonFullName);

            if (result)
            {
                Console.WriteLine("Информация обновлена успешно!");
            }
            else
            {
                Console.WriteLine("Организация не найдена. Информация не обновлена.");
            }
        }

        static void GetGoldenClient(List<Product> products, List<Client> clients, List<Order> orders)
        {
            var orderInfoCombine = new OrderInfoCombine(products, clients, orders);

            Console.WriteLine("Введите год:");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите месяц:");
            int month = int.Parse(Console.ReadLine());

            string goldenClientResult = orderInfoCombine.GetGoldenClient(year, month);

            Console.WriteLine(goldenClientResult);
        }
    }
}
