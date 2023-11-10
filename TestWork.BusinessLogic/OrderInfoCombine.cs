using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.BusinessLogic.Excel;
using TestWork.Core;
using TestWork.Core.Data;

namespace TestWork.BusinessLogic
{
    public class OrderInfoCombine
    {

        private List<Product> products;
        private List<Client> clients;
        private List<Order> orders;

        public OrderInfoCombine(List<Product> products, List<Client> clients, List<Order> orders)
        {
            this.products = products;
            this.clients = clients;
            this.orders = orders;
        }

        //для 2 запроса
        public List<OrderInfo> GetOrdersByProductName(string productName)
        {
            // Используем уже загруженные данные
            var query = from order in orders
                        join product in products on order.ProductCode equals product.ProductCode
                        join client in clients on order.ClientCode equals client.ClientCode
                        where product.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase)
                        select new OrderInfo
                        {
                            ClientName = client.OrganizationName,
                            Quantity = order.Quantity,
                            UnitPrice = product.UnitPrice,
                            OrderDate = order.OrderDate
                        };

            return query.ToList();
        }

        // для 4 запроса
        public List<Order> GetOrdersByYearAndMonth(int year, int month)
        {
            return orders
                .Where(order => order.OrderDate.Year == year && order.OrderDate.Month == month)
                .ToList();
        }

        public string GetGoldenClient(int year, int month)
        {
            var ordersByYearAndMonth = GetOrdersByYearAndMonth(year, month);

            var goldenClient = ordersByYearAndMonth
                .GroupBy(order => order.ClientCode)
                .OrderByDescending(group => group.Count())
                .FirstOrDefault()
                ?.Key;

            if (goldenClient.HasValue)
            {
                var client = clients.FirstOrDefault(c => c.ClientCode == goldenClient);
                return $"Золотой клиент за {year}-{month}: {client?.OrganizationName}";
            }

            return "Нет данных о заказах за указанный период";
        }


    }
}
